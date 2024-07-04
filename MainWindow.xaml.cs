using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Streamer_Hub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /**
         * - When program is exited without closing the twitch process, I think sometimes it still runs for a couple 
         *      more commands before it realizes it shouldn't be running anymore and then gets killed.
         * 
         * 
         * 
         * 
         */


        public Process process;


        



        public MainWindow()
        {
            InitializeComponent();

            process = new Process();


            //imageControl.Source = (ImageSource) FindResource("EldenRing Map.png");



        }


        private String RunCommand(string fileName, string args)  // original  -->  private String RunCommand(string fileName, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true // originally true, but I want to see it right now
            };
            
            var sb = new StringBuilder();
            Process process;

            using (process = new Process())
            {
                process.StartInfo = start;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (sender, eventArgs) =>
                {
                    sb.AppendLine(eventArgs.Data); //allow other stuff as well
                    //Debug.WriteLine("From Output: " + sb);
                };
                process.ErrorDataReceived += (sender, eventArgs) =>
                {
                    sb.AppendLine(eventArgs.Data); //For some reason some ffmpeg commands prints in the ErrorOutput section, not entirely sure why.
                    //Debug.WriteLine("From Error: " + sb);
                };

                if (process.Start())
                {
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Keep looping until we have text in the stringbuilder (this text HAS to show up before it can continue as it's the duration of the video.)
                    while (sb.ToString().Trim().Length == 0)
                        Task.Delay(20).Wait();

                    process.Kill(); // This is here because otherwise a bunch of cmd processes would just be open in task manager.
                }
            }
             

            // Once here, the process should now be killed, meaning I should be able to format this properly now
            // Since in the stringbuilder, the time of the video is always the first thing, we can just grab the first object in the array and ignore the rest.

            // Commented out for now, but this was used for testing the values. I shouldn't need to use this again tho.
            //String test = "150.215967\n\r\n\rD:\\Visual Studio Projects\\Video Trimming\\bin\\Debug\\net8.0 - windows >\n\r\n\r";
            //String[] lines = test.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);

            String finalString = sb.ToString();
            //String finalString = "beans";

            /**
            String[] lines = sb.ToString().Trim().Split(new String[] { Environment.NewLine }, StringSplitOptions.None);

            if (lines.Length > 1)
            {
                for (int i = 0; i < 50; i++)
                {
                    Debug.Write("THE RARE THING HAPPENED BUT THE CODED STILL WORKED, POG | THE RARE THING HAPPENED BUT THE CODED STILL WORKED, POG | ");
                    Debug.WriteLine("THE RARE THING HAPPENED BUT THE CODED STILL WORKED, POG | THE RARE THING HAPPENED BUT THE CODED STILL WORKED, POG");
                }

                rareErrorButton.Visibility = Visibility.Visible;
            }
            

            return lines[0].Trim();
             */

            return finalString;
        }





        private void Start_Bot_Click(object sender, RoutedEventArgs e)
        {









            /**
            // original code that does work, but I dont like it.

            //pack://application:,,,/Anime Organizer;component/Fonts/
            //pack://application:,,,/Streamer Hub;component/TwitchBot/app.js
            //pack://application:,,,/Streamer Hub;component/TwitchBot/EldenRing Map - Edit.png

            Uri pageUri = new Uri("/TwitchBot/twitchBot.js", UriKind.Relative);

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = @"/K node " + "\"" + Environment.CurrentDirectory + pageUri + "\"",
                //Arguments = @"/K node " + "\"" + str3 + "\"", // original that works
                //Arguments = @"/K node " + "\"" + pageUri + "\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true // originally true, but I want to see it right now
            };
            process.StartInfo = startInfo;

            process.OutputDataReceived += (sender, eventArgs) =>
            {
                //sb.AppendLine(eventArgs.Data); //allow other stuff as well
                Debug.WriteLine("From Output: " + eventArgs.Data);

                this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    tBlock.Text += "\n" + eventArgs.Data;
                }));

            };
            process.ErrorDataReceived += (sender, eventArgs) =>
            {
                //sb.AppendLine(eventArgs.Data); //For some reason some ffmpeg commands prints in the ErrorOutput section, not entirely sure why.
                Debug.WriteLine("From Error: " + eventArgs.Data);
                //1&#x0a;

                this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    tBlock.Text += "\n" + eventArgs.Data;
                }));

            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();


            Debug.WriteLine("EREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");

             */








        }

        private void Stop_Bot_Click(object sender, RoutedEventArgs e)
        {
            process.Close();
        }

    }
}