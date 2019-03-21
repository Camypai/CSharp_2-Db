using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using Charp_2_Db.Annotations;
using Charp_2_Db.Helpers;
using Charp_2_Db.Models;

namespace Charp_2_Db
{
    /// <summary>
    /// Контроллер для работы MVVM
    /// </summary>
    public class Controller : INotifyPropertyChanged
    {
        /// <summary>
        /// Хранилище сотрудников
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        /// <summary>
        /// Хранилище департаментов
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        /// <summary>
        /// Выбранный департамент
        /// </summary>
        public int SelectedItem
        {
            get => _selectedItem;
            set
            {
                Employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple(item => item.DepartmentId == value));
                CollectionViewSource.GetDefaultView(Employees).Refresh();

                _selectedItem = value;
            }
        }

        private ObservableCollection<Employee> _employee;
        private int _selectedItem;
        
        /// <summary>
        /// Форма добавления сотрудника
        /// </summary>
        private AddEmployee _addEmployee;
        
        /// <summary>
        /// Форма добавления департамента
        /// </summary>
        private AddDepartment _addDepartment;

        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private readonly JobDbContext _db;

        /// <summary>
        /// Команда сохранения
        /// </summary>
        private Command _saveCommand;

        /// <summary>
        /// Команда перезагрузки данных
        /// </summary>
        private Command _refreshCommand;

        /// <summary>
        /// Команда открытия формы добавления сотрудника
        /// </summary>
        private Command _addEmployeeCommand;

        /// <summary>
        /// Команда открытия формы добавления департамента
        /// </summary>
        private Command _addDepartmentCommand;


        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(q =>
        {
            _db.Save();
            Departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
            

            Employees = new ObservableCollection<Employee>(
                _db.EmployeeRepository.RetrieveMultiple());
        }));

        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(q =>
        {
            Employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            Departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }));

        public Command AddEmployee => _addEmployeeCommand ?? (_addEmployeeCommand = new Command(q =>
        {
            _addEmployee.ComboBox.ItemsSource = Departments;
            var dialogResult = _addEmployee.ShowDialog();

            if (dialogResult.Value)
            {
                Employees.Add(_db.EmployeeRepository.Retrieve(_addEmployee.Id.Value));
            }
        }));

        public Command AddDepartment => _addDepartmentCommand ?? (_addDepartmentCommand = new Command(q =>
        {
            var dialogResult = _addDepartment.ShowDialog();

            if (dialogResult.Value)
            {
                Departments.Add(_db.DepartmentRepository.Retrieve(_addDepartment.Id.Value));
            }
        }));

        public Controller()
        {
            _db = new JobDbContext();
            
            Departments =
                new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
            
            SelectedItem = Departments.FirstOrDefault().Id;

            _addEmployee = new AddEmployee(_db);
            _addDepartment = new AddDepartment(_db);
        }

        /// <summary>
        /// Событие изменения данных
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}