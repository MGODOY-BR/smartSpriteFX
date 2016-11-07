using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI
{
    /// <summary>
    /// Offers ways to use pattern message boxes.
    /// </summary>
    public static class MessageBoxUtil
    {
        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(String message, MessageBoxButtons options)
        {
            return MessageBoxUtil.Show(GetApplicationName(), message, options);
        }

        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(Exception exception)
        {
            return MessageBoxUtil.Show("Internal error", exception.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(String caption, Exception exception)
        {
            return MessageBoxUtil.Show(caption, exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(String caption, String message, MessageBoxButtons options)
        {
            MessageBoxIcon icon;

            switch (options)
            {
                case MessageBoxButtons.OK:
                    icon = MessageBoxIcon.Information;
                    break;
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.RetryCancel:
                    icon = MessageBoxIcon.Question;
                    break;
                default:
                    throw new NotSupportedException(options.ToString());
            }

            return MessageBoxUtil.Show(caption, message, options, icon);
        }

        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(string caption, string message, MessageBoxButtons options, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, options, icon);
        }

        /// <summary>
        /// Shows a message box
        /// </summary>
        public static DialogResult Show(string message, MessageBoxIcon icon)
        {
            return MessageBoxUtil.Show(typeof(MessageBoxUtil).Assembly.GetName().Name, message, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Gets the application name to show
        /// </summary>
        /// <returns></returns>
        private static string GetApplicationName()
        {
            return "smartSpriteFX";
        }

    }
}
