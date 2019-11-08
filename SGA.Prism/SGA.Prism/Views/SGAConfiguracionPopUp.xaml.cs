using System.Threading.Tasks;
using Xamarin.Forms;

namespace SGA.Prism.Views
{
    public partial class SGAConfiguracionPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SGAConfiguracionPopUp()
        {
            InitializeComponent();
            FondoPrimario.Focus();
        }

        //Para que al Salir del campo, se vaya al siguiente campo automáticamente
        private void FondoPrimario_Unfocused(object sender, FocusEventArgs e)
        {
            FondoSecun.Focus();
        }

        private void FondoSecun_Unfocused(object sender, FocusEventArgs e)
        {
            FondoTer.Focus();
        }

        private void FondoTer_Unfocused(object sender, FocusEventArgs e)
        {
            BtnPrimario.Focus();
        }

        private void BtnPrimario_Unfocused(object sender, FocusEventArgs e)
        {
            BtnSecun.Focus();
        }
        private void BtnSecun_Unfocused(object sender, FocusEventArgs e)
        {
            BtnTer.Focus();
        }

        private void BtnTer_Unfocused(object sender, FocusEventArgs e)
        {
            LabelPrimario.Focus();
        }

        private void LabelPrimario_Unfocused(object sender, FocusEventArgs e)
        {
            LabelSecun.Focus();
        }

        private void LabelSecun_Unfocused(object sender, FocusEventArgs e)
        {
            Danger.Focus();
        }

        private void Danger_Unfocused(object sender, FocusEventArgs e)
        {
            TextoPrimario.Focus();
        }

        private void TextoPrimario_Unfocused(object sender, FocusEventArgs e)
        {
            TextoSecun.Focus();
        }

        private void TextoSecun_Unfocused(object sender, FocusEventArgs e)
        {
            Acentuado.Focus();
        }

        private void Acentuado_Unfocused(object sender, FocusEventArgs e)
        {
            BotonGuardar.Focus();
        }
    }
}
