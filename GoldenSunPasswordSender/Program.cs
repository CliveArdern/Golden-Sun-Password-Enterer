using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GoldenSunPasswordSender
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void Main()
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.GetType()}{Environment.NewLine}{ex.Message}{Environment.NewLine}Press any key to exit.");
                Console.Read();
            }
        }

        private static void Run()
        {
            Console.WriteLine($"GOLDEN SUN PASSWORD INPUTTER{Environment.NewLine}============================{Environment.NewLine}{Environment.NewLine}" +
                              "Ensure your emulator is running and The Lost Age is at the password entry screen, then press any key to start.");
            Console.ReadKey();
            Console.WriteLine("Starting password entry. Do not touch the mouse, keyboard, or controllers.");
            Config config = Config.Load();
            BringToFront(config.Emulator);
            int col = 0;
            int row = 0;
            int lastCol = 0;
            int lastRow = 0;
            Thread.Sleep(1000);
            DateTime start = DateTime.UtcNow;
            foreach (char c in File.ReadAllText(config.Password).Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty))
            {
                col = Layout.positions[c].col;
                row = Layout.positions[c].row;
                if (col > lastCol)
                {
                    SendKeyWait(config.Right, col - lastCol);
                }
                else if (col < lastCol)
                {
                    SendKeyWait(config.Left, lastCol - col);
                }
                if (row > lastRow)
                {
                    SendKeyWait(config.Down, row - lastRow);
                }
                else if (row < lastRow)
                {
                    SendKeyWait(config.Up, lastRow - row);
                }
                SendKeyWait(config.Confirm, 1);
                lastCol = col;
                lastRow = row;
            }
            Console.WriteLine($"{Environment.NewLine}Password entry completed in {(DateTime.UtcNow - start).TotalSeconds:N2} seconds.{Environment.NewLine}Press any key to exit.");
            Console.Read();
        }

        private static void SendKeyWait(string key, int times)
        {
            for (int i = 0; i < times; i++)
            {
                SendKeys.SendWait(key);
            }
        }

        private static void BringToFront(string processName) => SetForegroundWindow(Process.GetProcessesByName(processName)[0].MainWindowHandle);
    }
}
