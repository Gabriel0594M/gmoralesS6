using gmoralesS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace gmoralesS6.Views;

public partial class vEstudiante : ContentPage
{

	private const string Url = "http://10.2.15.17/ws_uisrael/ws_estudiante.php";
	private readonly HttpClient cliente = new HttpClient();

	private ObservableCollection<Estudiante> _estudiante;
    public vEstudiante()
	{
		InitializeComponent();
		Mostrar();

    }



	public async void Mostrar()
    {
       var content = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estudiante = new ObservableCollection<Estudiante>(mostrarEstudiante);
		listaEstudiantes.ItemsSource = _estudiante;
	}

}