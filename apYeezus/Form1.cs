using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace apYeezus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WMPLib.IWMPPlaylist playList;

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            ofdAbrir.Title = "Abrir midia";
            ofdAbrir.Filter = "Arquivo mp4|*.mp4|Arquivo mp3|*.mp3|Arquivo MPEG-4|*.m4a"; //filtrar para exibir apenas arquivos de audio e video
            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                playList = player.playlistCollection.newPlaylist("lista");

                foreach (var arquivo in ofdAbrir.FileNames)
                {
                    playList.appendItem(player.newMedia(arquivo));
                    lstPlaylist.Items.Add(arquivo);

                    player.currentPlaylist = playList;
                    player.Ctlcontrols.play();
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(lstPlaylist.Items.Count > 0)
            {
                sfdSalvar.Title = "Salvar Playlist";
                sfdSalvar.Filter = "Arquivo texto|*.txt";
                if(sfdSalvar.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter arquivo = new StreamWriter(sfdSalvar.FileName, false);
                    for (int indice = 0; indice < lstPlaylist.Items.Count -1; indice++)
                    {
                        arquivo.WriteLine(lstPlaylist.Items[indice].ToString());
                    }
                    arquivo.Close();
                }
            }

        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            sfdSalvar.Title = "Abrir Playlist";
            sfdSalvar.Filter = "Arquivo texto|*.txt";
            ofdAbrir.Multiselect = false;
            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader arquivo = new StreamReader(ofdAbrir.FileName);
                while (arquivo.Peek() != -1)
                {
                    lstPlaylist.Items.Add(arquivo.ReadLine());
                }
                arquivo.Close();
            }
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if(lstPlaylist.Items.Count > 0)
            {
                player.URL = lstPlaylist.SelectedItem.ToString();
                player.Ctlcontrols.play();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            lstPlaylist.Items.Clear();
        }
    }
}
