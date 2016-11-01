/// <summary>
/// Usage			: Specialized Observable Collection
/// Domain			: Application.Types.ObservableCollectionEx 
/// Functional	 	: Special version of Observable Collection
/// 
/// Remarks			: Original the Observable Collection did NOT support AddRange, when adding this Method to an Observable Collection for Each item added with
///  				  AddRange by Default the CollectionChanged (and PropertyChanged) is fired, which means that for rendering purposes due to Binding to the 
/// 				  collection we would render the full view Over and Over again (multiple times) which is very costly. This is why this sealed class is
/// 			      introduced and actually is doing the operations on the underlying IEnumerable<T> and manually fire the CollectionChanged event with
///  				  Partial (Add Action is fired back) or Complete (Reset Action is fired back) only once when adding items with AddRange
/// 				  Also added internal sorting without copying (replacing) the whole collection (list) which is much faster and triggers the correct events
/// 				  IMPORTANT : If we need to trigger Complete on other actions (Like Remove, Replace) then we can extend this class also in the same manner
/// 							  as done for AddRange
/// </summary>
#region .NET Assemblies
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
#endregion 

#region <YourNameSpace> Assemblies
using MyMovieCollection.Implementation;
#endregion

namespace MyMovieCollection.Implementation
{
    public enum ChangeNotificationMode
    {
        Partial,
        Complete
    }

    /// <summary>
    /// Observable collection ex(tended)
    /// </summary>
    public sealed class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="<YourNameSpace>.Types.System.ObservableCollectionEx`1"/> class.
        /// </summary>
        public ObservableCollectionEx()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="<YourNameSpace>.Types.System.ObservableCollectionEx`1"/> class.
        /// </summary>
        /// <param name="list">List.</param>
        public ObservableCollectionEx(List<T> list) : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="<YourNameSpace>.Types.System.ObservableCollectionEx`1"/> class.
        /// </summary>
        /// <param name="collection">Collection.</param>
        public ObservableCollectionEx(IEnumerable<T> collection) : base(collection)
        {
        }
        #endregion

        /// <summary> 
        /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        /// </summary> 
        public void AddRange(
            IEnumerable<T> itemsToAdd,
            ChangeNotificationMode notificationMode = ChangeNotificationMode.Partial)
        {
            if (itemsToAdd != null)
            {
                CheckReentrancy();

                if (notificationMode == ChangeNotificationMode.Complete)
                {
                    foreach (var i in itemsToAdd)
                    {
                        Items.Add(i);
                    }

                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                    return;
                }

                int startIndex = Count;
                var changedItems = itemsToAdd is List<T> ? (List<T>)itemsToAdd : new List<T>(itemsToAdd);
                foreach (var i in changedItems)
                {
                    Items.Add(i);
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
            }
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector)
        {
            CheckReentrancy();
            InternalSort(Items.OrderBy(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <typeparam name="TKey2">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="thenByKeySelector"></param>
        public void Sort<TKey, TKey2>(Func<T, TKey> keySelector, Func<T, TKey2> thenByKeySelector)
        {
            CheckReentrancy();
            InternalSort(Items.OrderBy(keySelector).ThenBy(thenByKeySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            CheckReentrancy();
            InternalSort(Items.OrderBy(keySelector, comparer));
        }

        /// <summary>
        /// Sorts the items of the collection in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void SortDescending<TKey>(Func<T, TKey> keySelector)
        {
            CheckReentrancy();
            InternalSort(Items.OrderByDescending(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in descending order according to a key and a comparer
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        public void SortDescending<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            CheckReentrancy();
            InternalSort(Items.OrderByDescending(keySelector, comparer));
        }

        /// <summary>
        /// Moves the items of the collection so that their orders are the same as those of the items provided.
        /// </summary>
        /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
        private void InternalSort(IEnumerable<T> sortedItems)
        {
            CheckReentrancy();
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

