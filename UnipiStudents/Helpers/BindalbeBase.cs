using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Properties;

namespace UnipiStudents.Helpers
{
        /// <summary>
        /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
        /// </summary>
        public abstract class BindableBase : INotifyPropertyChanged
        {
            /// <summary>
            /// Occurs when a property value changes.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Checks if a property already matches a desired value. Sets the property and
            /// notifies listeners only when necessary.
            /// </summary>
            /// <typeparam name="T">Type of the property.</typeparam>
            /// <param name="storage">Reference to a property with both getter and setter.</param>
            /// <param name="value">Desired value for the property.</param>
            /// <param name="propertyName">Name of the property used to notify listeners. This
            /// value is optional and can be provided automatically when invoked from compilers that
            /// support CallerMemberName.</param>
            /// <returns>True if the value was changed, false if the existing value matched the
            /// desired value.</returns>
            protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

                storage = value;
                RaisePropertyChanged(propertyName);

                return true;
            }

            /// <summary>
            /// Checks if a property already matches a desired value. Sets the property and
            /// notifies listeners only when necessary.
            /// </summary>
            /// <typeparam name="T">Type of the property.</typeparam>
            /// <param name="storage">Reference to a property with both getter and setter.</param>
            /// <param name="value">Desired value for the property.</param>
            /// <param name="propertyName">Name of the property used to notify listeners. This
            /// value is optional and can be provided automatically when invoked from compilers that
            /// support CallerMemberName.</param>
            /// <param name="onChanged">Action that is called after the property value has been changed.</param>
            /// <returns>True if the value was changed, false if the existing value matched the
            /// desired value.</returns>
            protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

                storage = value;
                onChanged?.Invoke();
                RaisePropertyChanged(propertyName);

                return true;
            }

            /// <summary>
            /// Raises this object's PropertyChanged event.
            /// </summary>
            /// <param name="propertyName">Name of the property used to notify listeners. This
            /// value is optional and can be provided automatically when invoked from compilers
            /// that support <see cref="CallerMemberNameAttribute"/>.</param>
            protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            {
                //TODO: when we remove the old OnPropertyChanged method we need to uncomment the below line
                //OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
#pragma warning disable CS0618 // Type or member is obsolete
                OnPropertyChanged(propertyName);
#pragma warning restore CS0618 // Type or member is obsolete
            }

            /// <summary>
            /// Notifies listeners that a property value has changed.
            /// </summary>
            /// <param name="propertyName">Name of the property used to notify listeners. This
            /// value is optional and can be provided automatically when invoked from compilers
            /// that support <see cref="CallerMemberNameAttribute"/>.</param>
            [Obsolete("Please use the new RaisePropertyChanged method. This method will be removed to comply wth .NET coding standards. If you are overriding this method, you should overide the OnPropertyChanged(PropertyChangedEventArgs args) signature instead.")]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }

            /// <summary>
            /// Raises this object's PropertyChanged event.
            /// </summary>
            /// <param name="args">The PropertyChangedEventArgs</param>
            protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
            {
                PropertyChanged?.Invoke(this, args);
            }

            /// <summary>
            /// Raises this object's PropertyChanged event.
            /// </summary>
            /// <typeparam name="T">The type of the property that has a new value</typeparam>
            /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
            [Obsolete("Please use RaisePropertyChanged(nameof(PropertyName)) instead. Expressions are slower, and the new nameof feature eliminates the magic strings.")]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
            {
                var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
                OnPropertyChanged(propertyName);
            }
        }

    public static class PropertySupport
    {
        /// <summary>
        /// Extracts the property name from a property expression.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        ///     Not a <see cref="MemberExpression"/><br/>
        ///     The <see cref="MemberExpression"/> does not represent a property.<br/>
        ///     Or, the property is static.
        /// </exception>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            return ExtractPropertyNameFromLambda(propertyExpression);
        }

        /// <summary>
        /// Extracts the property name from a LambdaExpression.
        /// </summary>
        /// <param name="expression">The LambdaExpression</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="expression"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the expression is:<br/>
        ///     The <see cref="MemberExpression"/> does not represent a property.<br/>
        ///     Or, the property is static.
        /// </exception>
        internal static string ExtractPropertyNameFromLambda(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException();

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException();

            var getMethod = property.GetMethod;
            if (getMethod.IsStatic)
                throw new ArgumentException();

            return memberExpression.Member.Name;
        }

    }
}
