using gmoralesS6.Models;
using System.Net;

namespace gmoralesS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    // Android emulator: 10.0.2.2 apunta al localhost del PC.
    //private static readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
       // ? "http://10.0.2.2/ws_uisrael/ws_estudiante.php"
       // : "http://10.2.4.30/ws_uisrael/ws_estudiante.php";

    public vActualizarEliminar(Estudiante estudiante)
    {
        InitializeComponent();

        // Cargar los datos del estudiante seleccionado
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre;
        txtApellido.Text = estudiante.apellido;
        txtEdad.Text = estudiante.edad.ToString();
    }

    private void txtEdad_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Solo permitir dígitos
        if (!string.IsNullOrEmpty(e.NewTextValue) && !e.NewTextValue.All(char.IsDigit))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // El PHP lee el PUT desde la URL ($_GET)
            string urlPut = "http://10.2.4.30/ws_uisrael/ws_estudiante.php" +
                "?codigo=" + Uri.EscapeDataString(txtCodigo.Text) +
                "&nombre=" + Uri.EscapeDataString(txtNombre.Text ?? "") +
                "&apellido=" + Uri.EscapeDataString(txtApellido.Text ?? "") +
                "&edad=" + Uri.EscapeDataString(txtEdad.Text ?? "");

            WebClient cliente = new WebClient();
            cliente.UploadValues(urlPut, "PUT", new System.Collections.Specialized.NameValueCollection());

            await DisplayAlertAsync("Éxito", "Estudiante actualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlertAsync("Confirmar",
            "¿Seguro que deseas eliminar a " + txtNombre.Text + " " + txtApellido.Text + "?",
            "Sí", "No");
        if (!confirmar) return;

        try
        {
            // El PHP lee el DELETE desde la URL ($_GET)
            string urlDelete = "http://10.2.4.30/ws_uisrael/ws_estudiante.php" + "?codigo=" + Uri.EscapeDataString(txtCodigo.Text);

            WebClient cliente = new WebClient();
            cliente.UploadValues(urlDelete, "DELETE", new System.Collections.Specialized.NameValueCollection());

            await DisplayAlertAsync("Éxito", "Estudiante eliminado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", ex.Message, "OK");
        }
    }
}
