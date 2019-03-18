using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Charp_2_Db.Helpers;
using Charp_2_Db.Models;

namespace Charp_2_Db
{
    /// <summary>
    /// Контроллер для работы MVVM
    /// </summary>
    public class Controller
    {
        private readonly DbContext _db;
        /// <summary>
        /// Команда сохранения
        /// </summary>
        private Command _saveCommand;
        /// <summary>
        /// Команда перезагрузки данных
        /// </summary>
        private Command _refreshCommand;

        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(q =>
        {
            _db.EmployeeRepository.Save(Employees);
            _db.DepartmentRepository.Save(Departments);
            Employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            Departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }));

        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(q =>
        {
            Employees = new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());
            Departments = new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }));

        /// <summary>
        /// Хранилище сотрудников
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        /// <summary>
        /// Хранилище департаментов
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        public Controller()
        {
            _db = new DbContext();
            
            Employees =
                new ObservableCollection<Employee>(_db.EmployeeRepository.RetrieveMultiple());

            Departments =
                new ObservableCollection<Department>(_db.DepartmentRepository.RetrieveMultiple());
        }
    }
}