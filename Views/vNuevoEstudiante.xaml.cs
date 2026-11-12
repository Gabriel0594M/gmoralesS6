using System.Net;

namespace gmoralesS6.Views;

public partial class vNuevoEstudiante : ContentPage
{
	public vNuevoEstudiante()
	{
		InitializeComponent();
	}

    private void txtEdad_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Solo permitir dígitos
        if (!string.IsNullOrEmpty(e.NewTextValue) && !e.NewTextValue.All(char.IsDigit))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }

    // Android emulator: 10.0.2.2 apunta al localhost del PC.
   // private static readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
     //   ? "http://10.0.2.2/ws_uisrael/ws_estudiante.php"
     //   : "http://10.2.4.30/ws_uisrael/ws_estudiante.php";

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            cliente.UploadValues("http://10.2.4.30/ws_uisrael/ws_estudiante.php", "POST", parametros);

            await DisplayAlertAsync("Éxito", "Estudiante agregado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", ex.Message, "OK");
        }
    }
}