using gmoralesS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace gmoralesS6.Views;

public partial class vEstudiante : ContentPage
{

	// Android emulator: 10.0.2.2 apunta al localhost del PC.
	// Windows: localhost. Celular físico: usar la IP del PC (ej. 10.2.4.30).
	//private static readonly string Url = DeviceInfo.Platform == DevicePlatform.Android
		//? "http://10.0.2.2/ws_uisrael/ws_estudiante.php"
		//: "http://10.2.4.30/ws_uisrael/ws_estudiante.php";

    
	private readonly HttpClient cliente = new HttpClient();

	private ObservableCollection<Estudiante> _estudiante;
    public vEstudiante()
	{
		InitializeComponent();
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Mostrar();
	}



	public async void Mostrar()
    {
		try
		{
			var content = await cliente.GetStringAsync("http://10.2.4.30/ws_uisrael/ws_estudiante.php");
			List<Estudiante> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
			_estudiante = new ObservableCollection<Estudiante>(mostrarEstudiante);
			listaEstudiantes.ItemsSource = _estudiante;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error de conexión", ex.Message, "OK");
		}
	}

	private async void btnAgregar_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new vNuevoEstudiante());
	}

	private async void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is not Estudiante seleccionado) return;

		// Quitar la selección para poder volver a tocar la misma fila
		listaEstudiantes.SelectedItem = null;

		await Navigation.PushAsync(new vActualizarEliminar(seleccionado));
	}

}