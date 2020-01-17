using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;
using System.Timers;
using System.IO;


namespace DebsExec
{
  
        public class Deb
        {

            readonly Timer _timer;

            public Deb()
            {

                _timer = new Timer(6000) { AutoReset = false };
                _timer.Elapsed += timer_Elapsed;

            }

            private void timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                BooksIntegonLibrary.checkDate();

            }
            public void Start() { _timer.Start(); }

            public void Stop() { _timer.Stop(); }







          
        }
    }



