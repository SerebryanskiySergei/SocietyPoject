using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Engine;
using World;
using World.Characters;

namespace NewSocietyProject
{
    public partial class Form1 : Form
    {
        Kingdom _world;
        Graphics _g1;
        Graphics _g2;
        DrawAll _drawing;
        Bitmap _b1, _b2;
        bool _changeBitmap;
        Thread _thread;
        private bool _started = false;
        private bool _stopped = false;
        private bool _shutdown = false;
        private int _speed= 30;
        /// <summary>
        /// How many seconds thread will sleep.
        /// </summary>
        private int Speed
        {
            get { return _speed; }
            set
            {
                if (value >= 0) _speed = value;
                else
                    _speed = 10;
            }
        }

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            drawAreaPB.Size = Size;
            drawAreaPB.SizeMode = PictureBoxSizeMode.Normal;
            _thread = new Thread(new ThreadStart(ThreadWork));
        }

        /// <summary>
        /// Threads the work.
        /// </summary>
        void ThreadWork()
        {
            while (true)
            {
                if (_shutdown)
                    return;
                if (!_stopped)
                {
                    MainEngine.ProcessLogic(_world);
                    drawAreaPB.Image = _changeBitmap ? _b1 : _b2;
                    _drawing.AllWorld(_world, _changeBitmap ? _g2 : _g1);
                    _changeBitmap = !_changeBitmap;
                }
                Thread.Sleep(Speed);
            }
        }

        /// <summary>
        /// Handles the Click event of the population10ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void population10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _world = new Kingdom(10, drawAreaPB.Width, drawAreaPB.Height);
            _drawing.AllWorld(_world, _g2);

        }

        /// <summary>
        /// Handles the Click event of the popylation50ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void popylation50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _world = new Kingdom(50, drawAreaPB.Width, drawAreaPB.Height);
            _drawing.AllWorld(_world, _g2);

        }

        /// <summary>
        /// Handles the Click event of the population100ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void population100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _world = new Kingdom(100, drawAreaPB.Width, drawAreaPB.Height);
            _drawing.AllWorld(_world, _g2);

        }

        /// <summary>
        /// Handles the Click event of the startMovingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void startMovingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_started)
            {
                _thread.Start();
                _started = true;

            }
            if (_stopped)
            {
                _stopped = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the stopMovingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void stopMovingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _thread.Abort();
        }

        /// <summary>
        /// Handles the Click event of the pauseToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stopped = true;
        }

        /// <summary>
        /// Handles the FormClosed event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _shutdown = true;
            if (_started)
                _thread.Abort();
        }

        /// <summary>
        /// Draw world.
        /// </summary>
        private class DrawAll
        {
            Bitmap warriorImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageWarrior"]);
            Bitmap traderImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageTrader"]);
            Bitmap robberImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageRobber"]);
            Bitmap craftsmanImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageCraftsman"]);
            Bitmap peasantFreeImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImagePeasantFree"]);
            //Bitmap peasantWorkingImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImagePeasantWorking"]);
            //Bitmap peasantGoingImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImagePeasantGoing"]);
            Bitmap CastleImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageCastle"]);
            //Bitmap FarmImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageFarm"]);
            Bitmap CrafthouseImage = new Bitmap(System.Configuration.ConfigurationManager.AppSettings["ImageCrafthouse"]);
            private Size _iconSize;
            private Size _castleSize;
            public DrawAll()
            {
                warriorImage.MakeTransparent(Color.White);
                peasantFreeImage.MakeTransparent(Color.White);
                //peasantGoingImage.MakeTransparent(Color.White);
                //peasantWorkingImage.MakeTransparent(Color.White);
                traderImage.MakeTransparent(Color.White);
                robberImage.MakeTransparent(Color.White);
                craftsmanImage.MakeTransparent(Color.White);
                CastleImage.MakeTransparent(Color.White);
                _iconSize = new Size(30, 30);
                _castleSize = new Size(200, 200);
            }
            /// <summary>
            /// Draw world on graphics g.
            /// </summary>
            /// <param name="world">The world.</param>
            /// <param name="g">The g.</param>
            public void AllWorld(Kingdom world, Graphics g)
            {
                g.Clear(Color.White);
                g.DrawImage(CastleImage, world.GetHabitat().Castle);
                g.DrawImage(CrafthouseImage, world.GetHabitat().CrHouse);
                g.DrawEllipse(new Pen(Color.Aquamarine, 2), world.GetHabitat().Farm);
                g.FillEllipse(new SolidBrush(Color.Chartreuse), world.GetHabitat().Farm);
                foreach (Person man in world.GetDictionaryOfCharacters().Values)
                {
                    switch (man.GetProfession())
                    {
                        case Profession.Warrior:
                            g.DrawImage(warriorImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                _iconSize.Width);
                            break;
                        case Profession.Trader:
                            g.DrawImage(traderImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                _iconSize.Width);
                            break;
                        case Profession.Robber:
                            g.DrawImage(robberImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                _iconSize.Width);
                            break;
                        case Profession.Craftsman:
                            g.DrawImage(craftsmanImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                _iconSize.Width);
                            break;
                        case Profession.Peasant:
                            if (man.GetStatus() == State.Moving)
                            {
                                if (man.GetFillingBag() > 10)
                                    g.DrawImage(peasantFreeImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                        _iconSize.Width);
                                else
                                    g.DrawImage(peasantFreeImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                    _iconSize.Width);
                                break;
                            }
                            if (man.GetStatus() == State.Working)
                            {
                                g.DrawImage(peasantFreeImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                    _iconSize.Width);
                                break;
                            }
                            g.DrawImage(peasantFreeImage, man.GetLocation().X, man.GetLocation().Y, _iconSize.Height,
                                _iconSize.Width);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the drawAreaPB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void drawAreaPB_SizeChanged(object sender, EventArgs e)
        {
            
            _b1 = new Bitmap(drawAreaPB.Width, drawAreaPB.Height);
            _b2 = new Bitmap(drawAreaPB.Width, drawAreaPB.Height);
            _g1 = Graphics.FromImage(_b1);
            _g2 = Graphics.FromImage(_b2);
            drawAreaPB.Image = _b1;
            _changeBitmap = true;
            _drawing = new DrawAll();
            drawAreaPB.Image = _b2;
            _thread = new Thread(ThreadWork);
        }

        /// <summary>
        /// Move faster.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Speed -= 50;
        }

        /// <summary>
        /// Move slowly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Speed += 50;
        }
        
    }
}
