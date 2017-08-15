using System;
using System.Drawing;
using System.Windows.Forms;

namespace MRO
{
    public partial class frmShutDown : Form
    {
        //Bitmap animatedImage = new Bitmap("5.gif");
        Bitmap animatedImage;
        bool currentlyAnimating = false;
        public frmMain _frmMain;
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {

                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {

            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //Begin the animation.
            AnimateImage();

            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            e.Graphics.DrawImage(this.animatedImage, new Point(0, 0));
        }

        public frmShutDown()
        {
            InitializeComponent();

            /*this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            animatedImage = Properties.Resources.ExitGif;
            this.Width = animatedImage.Width;
            this.Height = animatedImage.Height;
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;*/
        }

        private void frmShutDown_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmShutDown.Hide();
        }
    }
}
