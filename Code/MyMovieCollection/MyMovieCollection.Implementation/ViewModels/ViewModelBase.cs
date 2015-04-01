using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation.ViewModels
{
    /// <summary>
    /// ViewModelBase
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// Fires when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "TheFactorM.Device.Logging.ApplicationLog.WriteInformational(System.String,System.Object[])", Justification = "YoupH:wel")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PropertyChanged", Justification = "YoupH: Technical text")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Is needed to support RaisePropertyChanged")]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <remarks>Uses OnPropertyChanged() and passes null if expression is invalid</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "YoupH: No")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "YoupH: Generics FTW")]
        protected void OnPropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> expression)
        {
            string propertyName = null;
            if (null != expression)
            {
                var member = expression.Body as System.Linq.Expressions.MemberExpression;
                if (null != member)
                {
                    propertyName = member.Member.Name;
                }
            }
            OnPropertyChanged(propertyName);
        }

        #endregion
    }
}
