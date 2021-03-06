﻿using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Forms;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new SelectModeScreenForm());

            ReleaseTempFiles();
        }

        /// <summary>
        /// Releases the temporary files
        /// </summary>
        private static void ReleaseTempFiles()
        {
            foreach (var file in Directory.GetFiles("Temp"))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    // Errors here can't turn away the flow
                }
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;

            if (exception is OverflowException)
            {
                MessageBoxUtil.Show(
                    "Too big image!!!!",
                    "Image too big for the available memory. Please, use some image editor to make it lighter and try again",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (exception is ArgumentException)
            {
                MessageBoxUtil.Show("Required information", exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (exception is ApplicationException)
            {
                MessageBoxUtil.Show("Required information", exception);
            }
            else
            {
                MessageBoxUtil.Show(exception);
            }
        }
    }
}
