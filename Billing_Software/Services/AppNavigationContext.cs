using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Billing_Software
{
    /// <summary>
    /// ApplicationContext that lets the app switch the "main" form without exiting the message loop.
    /// Initialize Navigator in Program.Main with an AppNavigationContext instance.
    /// </summary>
    public sealed class AppNavigationContext : ApplicationContext
    {
        public AppNavigationContext(Form initial)
        {
            if (initial is null) throw new ArgumentNullException(nameof(initial));
            MainForm = initial;
            MainForm.FormClosed += OnMainFormClosed;
            MainForm.Show();
        }

        private void OnMainFormClosed(object? sender, FormClosedEventArgs e)
        {
            // When the main form closes, shut down the message loop.
            ExitThread();
        }

        /// <summary>
        /// Switch visible main form to <paramref name="next"/>.
        /// Hides the previous main form and makes <paramref name="next"/> the current MainForm.
        /// </summary>
        public void SwitchTo(Form next)
        {
            if (next is null) throw new ArgumentNullException(nameof(next));

            if (MainForm != null)
            {
                MainForm.FormClosed -= OnMainFormClosed;
                try { MainForm.Hide(); } catch { /* best effort */ }
            }

            MainForm = next;
            MainForm.FormClosed += OnMainFormClosed;
            MainForm.Show();
        }
    }

    /// <summary>
    /// Global navigation helper used from forms.
    /// Call Navigator.Initialize(...) from Program.Main once at startup.
    /// </summary>
    public static class Navigator
    {
        private static AppNavigationContext? _context;

        public static void Initialize(AppNavigationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public static void SwitchTo(Form form)
        {
            if (form is null) throw new ArgumentNullException(nameof(form));

            // DIAGNOSTIC: log the call stack so we can find who is auto-opening Form2.
            // Run the app under the debugger and check Output -> Debug to see this trace.
            Debug.WriteLine("Navigator.SwitchTo called. Target form: " + form.GetType().FullName);
            Debug.WriteLine("Call stack:\n" + Environment.StackTrace);

            // Preferred path: use the initialized AppNavigationContext
            if (_context != null)
            {
                _context.SwitchTo(form);
                return;
            }

            // Fallback: hide current visible top-level form and show requested form
            try
            {
                var current = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Visible && f != form);
                if (current != null)
                {
                    try { current.Hide(); } catch { /* best effort */ }
                }

                form.FormClosed += (s, e) =>
                {
                    var anyVisible = Application.OpenForms.Cast<Form>().Any(f => f.Visible);
                    if (!anyVisible) Application.ExitThread();
                };

                form.Show();
            }
            catch
            {
                // Last-resort show
                form.Show();
            }
        }
    }
}