using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Charp_2_Db.Models;

namespace Charp_2_Db
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DbContext _db = new DbContext();
        private ListCollectionView _data;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _data = new ListCollectionView(_db.EmployeeRepository.RetrieveMultiple().ToList());
            
            DataGrid.ItemsSource = _data;
        }

        private void miSave_OnClick(object sender, RoutedEventArgs e)
        {
            _db.EmployeeRepository.Save((IEnumerable<Employee>)_data.SourceCollection);
            _data = new ListCollectionView(_db.EmployeeRepository.RetrieveMultiple().ToList());
        }

        private void miRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            _data = new ListCollectionView(_db.EmployeeRepository.RetrieveMultiple().ToList());
        }
    }
}