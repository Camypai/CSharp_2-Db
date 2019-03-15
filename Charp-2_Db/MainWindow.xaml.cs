using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Department> _departments;
        
        public MainWindow()
        {
            InitializeComponent();

            _employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            _departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
            
            DataGridEmployees.ItemsSource = _employees;
            DgDepartment.ItemsSource = _departments;

            DataGridDepartments.ItemsSource = _departments;
        }

        private void miSave_OnClick(object sender, RoutedEventArgs e)
        {
            _db.EmployeeRepository.Save(_employees);
            _db.DepartmentRepository.Save(_departments);
            _employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            _departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }

        private void miRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            _employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            _departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }
    }
}