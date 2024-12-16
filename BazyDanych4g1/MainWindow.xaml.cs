using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BazyDanych4g1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private dbContext _context = new dbContext();

    public MainWindow()
    {
        InitializeComponent();

        _context.Films.ToList().ForEach(film =>

            film.Category = _context.Categories.ToList().FirstOrDefault(category => category.Id == film.CategoryId)
        );


        data.ItemsSource = _context.Films.ToList();
        category.ItemsSource = _context.Categories.Select(c => c.Name).ToList();
        category.SelectedIndex = 0;


        List<Category> categories = _context.Categories.ToList();
        categories.Insert(0, new Category {Id=0, Name = "All" });
        categoryFilter.ItemsSource = categories;
        categoryFilter.SelectedIndex = 0;
    }

    private void addFilm_Click(object sender, RoutedEventArgs e)
    {
        var Film = new Film
        {
            Title = filmName.Text,
            Year = int.Parse(yearFilm.Text),
            CategoryId = _context.Categories.ToList().FirstOrDefault(c => c.Name == category.Text).Id
        };
        _context.Films.Add(Film);
        _context.SaveChanges();
        data.ItemsSource = _context.Films.ToList();

    }

    private void CategoryFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cat =(Category) categoryFilter.SelectedItem;
        if(cat != null)
            if (cat.Id== 0)
                data.ItemsSource = _context.Films.ToList();
            else
                data.ItemsSource = _context.Films.Where(f => f.CategoryId == cat.Id).ToList();
        
    }

    private void Delte_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedFilms = data.SelectedItems.OfType<Film>().ToList();
        foreach (var n in selectedFilms)
        {
            _context.Films.Remove(n);
        }
            _context.SaveChanges();
            data.ItemsSource = _context.Films.ToList();
        
    }

    private void Edit_OnClick(object sender, RoutedEventArgs e)
    {
        wrapEdit.Visibility = Visibility.Visible;
        var wybranyFilm = (Film)  data.SelectedItem ;
        
        filmNameEdit.Text = wybranyFilm.Title;
        yearFilmEdit.Text = wybranyFilm.Year.ToString();
        categoryEdit.Text = _context.Categories.ToList().FirstOrDefault(c => c.Id == wybranyFilm.CategoryId).Name;
        _context.SaveChanges();
        data.ItemsSource = _context.Films.ToList();
    }

    private void Save_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}