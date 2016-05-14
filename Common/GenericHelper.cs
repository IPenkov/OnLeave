namespace Common
{
    #region Using Directives

    using System;
    using System.IO;
    using System.Linq.Expressions;

    #endregion

    public static class GenericHelper
    {
        // <summary>
        // Get the name of a static or instance property from a property access lambda.
        // </summary>
        // <typeparam name="T">Type of the property</typeparam>
        // <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        // <returns>The name of the property</returns>
        public static string GetPropertyName<T>(this object obj, Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        /// <summary>
        /// Bitmap to byte method.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this System.Drawing.Bitmap image)
        {
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {

                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                data = memoryStream.ToArray();

            }

            return data;

        }

    }
}
