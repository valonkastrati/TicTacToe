using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// objekti qe gjeneron numra te rastesishem
        /// </summary>
        Random rasti = new Random();
        
        /// <summary>
        /// numeruesi i levizjeve, per te ditur kur mbaron loja
        /// </summary>
        int levizje = 0;


        /// <summary>
        /// kjo metode inicon variablet ne fillim te lojes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilloLojen_Click(object sender, EventArgs e)
        {
            // ketu fillon loja
            levizje = 0;
            foreach (Control b in panelLoja.Controls)
            {
                Button b1 = (b as Button);
                b1.Text = "";
            }
            // paneli me butona behet enabled
            panelLoja.Enabled = true;

            //caktohet kush e ka radhen (fillon 0 gjithmone)
            lblRadha.Text = "0";

            labelFituesi.Text = "Fitues: ???";

            // nese fillon kompjuteri
            if (checkKunderKompj.Checked)
                LuajVete();
        }

        /// <summary>
        /// Kjo metode thirret ne Click te seciles fushe te lojes
        /// </summary>
        /// <param name="sender">butoni qe eshte klikuar</param>
        /// <param name="e"></param>
        private void UKlikuaNjeButon(object sender, EventArgs e)
        {

            //se pari e gjejme butonin qe u klikua
            var b = sender as Button;

            //nese ka qene i klikuar me heret, mos bej asgje
            if (b.Text != "")
                return;

            
            levizje++;

            //vendose ne buton 0 ose X, varesisht kush e ka pasur radhen
            b.Text = lblRadha.Text;

            //duhet te shohim mos ka fitues
            string pergjigja = ShikoAKaFitues();

            if (pergjigja != "")
            {
                // nese Po, shfaqe mesazhin
                labelFituesi.Text = pergjigja;

                // beje disable panelin e lojes
                panelLoja.Enabled = false;
                return;
            }

            // perndryshe, vazhdon loja
            // nderrohet radha (nga X ne 0 ose nga 0 ne X)
            if (lblRadha.Text == "0")
                lblRadha.Text = "X";
            else
                lblRadha.Text = "0";


            //nese luajme kunder kompjuterit dhe ai e ka radhen
            if (checkKunderKompj.Checked && lblRadha.Text == "0")
                LuajVete();

            if (checkKunderKompjX.Checked && lblRadha.Text == "X")
                LuajVete();

        }

        /// <summary>
        /// Kjo metode thirret per te bere levizje te rastesishme 
        /// kur luajme kunder kompjuterit
        /// </summary>
        private void LuajVete()
        {
            // duhet te tentojme disa here 
            // derisa te "zgjedhim" ne menyre te rastesishme
            // nje fushe te lire
            while (true)
            {
                int numri = rasti.Next(1, 10); //1-9
                string emri = "button" + numri;
                var ctl = panelLoja.Controls[emri];
                // nese eshte gjetur nje fushe e lire aplikohet levizja
                if (ctl.Text == "")
                {
                    // bejme nje refresh 
                    Text = "Duke menduar...";
                    this.Refresh();
                    // bejme sikur kompjuteri ka menduar 1 sekonde
                    Thread.Sleep(1000);
                    Text = "Tic Tac Toe";

                    // kompjuteri gjoja e realizon levizjen
                    UKlikuaNjeButon(ctl, null);
                    return;
                }
            }
        }

        /// <summary>
        /// Kjo metode shikon pas cdo levizje, a ka fitues
        /// nese po, kthen mesazhin
        /// </summary>
        /// <returns>Mesazhi i kthyer, tregon 
        /// fituesin (0 ose X)</returns>
        private string ShikoAKaFitues()
        {
            //rreshti i pare (butonat 1,2,3)
            if (AJaneTeNjejte(button1, button2, button3))
                return $"Fitues eshte:{button1.Text}";

            //rreshti i dyte (butonat 4,5,6)
            if (AJaneTeNjejte(button4, button5, button6))
                return $"Fitues eshte:{button4.Text}";

            //rreshti i trete (butonat 7,8,9)
            if (AJaneTeNjejte(button7, button8, button9))
                return $"Fitues eshte:{button7.Text}";

            //shtylla e pare (butonat 1,4,7)
            if (AJaneTeNjejte(button1, button4, button7))
                return $"Fitues eshte:{button1.Text}";

            //shtylla e dyte (butonat 2,5,8)
            if (AJaneTeNjejte(button2, button5, button8))
                return $"Fitues eshte:{button2.Text}";

            //shtylla e trete (butonat 3,6,9)
            if (AJaneTeNjejte(button3, button6, button9))
                return $"Fitues eshte:{button3.Text}";

            //diagonalja e pare (butonat 1,5,9)
            if (AJaneTeNjejte(button1, button5, button9))
                return $"Fitues eshte:{button1.Text}";

            //diagonalja e dyte (butonat 3,5,7)
            if (AJaneTeNjejte(button3, button5, button7))
                return $"Fitues eshte:{button3.Text}";

            if (levizje == 9)
                return "Loja barazim";

            return "";
        }

        /// <summary>
        /// Kjo metode kthen true nese 3 
        /// butonat a,b dhe c jane X (ose 0) njekohesisht
        /// </summary>
        /// <param name="a">Butoni i pare</param>
        /// <param name="b">Butoni i dyte</param>
        /// <param name="c">Butoni i trete</param>
        /// <returns>True nese 3 butonat jane X (ose 0)</returns>
        private bool AJaneTeNjejte(Button a, Button b, Button c)
        {
            if (a.Text == b.Text && b.Text == c.Text && a.Text != "")
                return true;

            return false;
        }

        private void checkKunderKompj_CheckedChanged(object sender, EventArgs e)
        {
            // per te mos lejuar qe te selektohen te dy checkbox-at
            if (checkKunderKompj.Checked && checkKunderKompjX.Checked)
                checkKunderKompjX.Checked = false;
        }

        private void checkKunderKompjX_CheckedChanged(object sender, EventArgs e)
        {
            // per te mos lejuar qe te selektohen te dy checkbox-at
            if (checkKunderKompjX.Checked && checkKunderKompj.Checked)
                checkKunderKompj.Checked = false;
        }
    }
}
