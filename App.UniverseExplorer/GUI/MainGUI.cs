using Terminal.Gui;
using System;
using App.Core.GUI;
using Mono.Terminal;
using Attribute = Terminal.Gui.Attribute;


namespace App.UniverseExplorer
{
    public class MainGUI : CLIWindow
    {
        class Box10x : View
        {
            public Box10x(int x, int y) : base(new Rect(x, y, 10, 10))
            { }

            public override void Redraw(Rect region)
            {
                Driver.SetAttribute(ColorScheme.Focus);

                for (int y = 0; y < 10; y++)
                    {
                        Move(0, y);
                        for (int x = 0; x < 10; x++)
                            {
                                Driver.AddRune((Rune) ('0' + (x + y) % 10));
                            }
                    }
            }
        }

        class Filler : View
        {
            public Filler(Rect rect) : base(rect)
            {
                
            }

            public override void Redraw(Rect region)
            {
                Driver.SetAttribute(ColorScheme.Focus);
                var f = Frame;

                for (int y = 0; y < f.Width; y++)
                    {
                        Move(0, y);
                        for (int x = 0; x < f.Height; x++)
                            {
                                Rune r;
                                switch (x % 3)
                                    {
                                        case 0:
                                            r = '.';
                                            break;
                                        case 1:
                                            r = 'o';
                                            break;
                                        default:
                                            r = 'O';
                                            break;
                                    }

                                Driver.AddRune(r);
                            }
                    }
            }
        }

        static void ShowEntries(View container)
        {
            bool timer(MainLoop caller)
            {
               // progress.Pulse();
                return true;
            }

            Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(300), timer);

            // Add some content
            container.Add(
                new Button("Ok") {X = 3, Y = 19},
                new Button("Cancel") {X = 10, Y = 19},
                new Label("Press F9 (on Unix ESC+9 is an alias) to activate the menubar") {X = 3, Y = 22}
            );
        }

        public static Label ml2;

        static bool Quit()
        {
            var n = MessageBox.Query(50, 7, "Quit Explorer", "Are you sure you want to quit this demo?", "Yes", "No");
            return n == 0;
        }

        static void Close()
        {
            MessageBox.ErrorQuery(50, 7, "Error", "There is nothing to close", "Ok");
        }

        public static Label ml;

        public override void OnWindowLoad()
        {
            //Application.UseSystemConsole = true;
            Application.Init();

            var top = Application.Top;
            var tframe = top.Frame;

            var win = new Window("Universe explorer")
                {
                    X = 0,
                    Y = 1,
                    Width = Dim.Fill(),
                    Height = Dim.Fill() - 1,
                    ColorScheme = new ColorScheme()
                        {
                            Normal = new Attribute(Color.White),
                            Disabled = new Attribute(Color.Blue),
                            Focus = new Attribute(Color.Blue),
                            HotFocus = new Attribute(Color.BrighCyan)
                        }

                };
            var menu = new MenuBar(new MenuBarItem[]
                {
                    new MenuBarItem("Options", new []
                        {
                            new MenuItem("_Exit", "Exit the app", () => Application.Shutdown())
                        }),
                });

            ShowEntries(win);
            int count = 0;
            ml = new Label(new Rect(3, 17, 47, 1), "Mouse: ");
            Application.RootMouseEvent += delegate(MouseEvent me)
                {
                    ml.Text = $"Mouse: ({me.X},{me.Y}) - {me.Flags} {count++}";
                };

            win.Add(ml);

            top.Add(win, menu);
            top.Add(menu);
            Application.Run();
        }
    }
}