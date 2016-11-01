using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyMovieCollection.Implementation;

namespace MyMovieCollection.Droid
{
    /// <summary>
    /// 
    /// </summary>
    [Application]
    class MyMovieCollectionApplication: Application
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="javaReference"></param>
        /// <param name="transfer"></param>
        public MyMovieCollectionApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            Setup.Initialize();
        }
    }
}