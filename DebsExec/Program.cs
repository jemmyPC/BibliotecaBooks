using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DebsExec
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>                                     //1
            {
                x.Service<Deb>(s =>                                          //2
                {
                    s.ConstructUsing(name => new Deb());                     //3
                    s.WhenStarted(tc => tc.Start());                         //4
                    s.WhenStopped(tc => tc.Stop());                          //5
                });
                x.RunAsLocalSystem();                                        //6

                x.SetDescription("Debs For Library");                   //7
                x.SetDisplayName("BooksLibrary");                                  //8
                x.SetServiceName("BooksLibrary");                                  //9
            });                                                             //10

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Environment.ExitCode = exitCode;

        }
    }
}
