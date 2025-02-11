﻿using ExpensesApp.Models;
using ExpensesApp.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Resource = ExpensesApp.Resources.Resources;
using static ExpensesApp.Helper.EqualityComparer;


namespace ExpensesApp.ViewModels
{
    public class NewExpensesViewModel : ViewModelBase
    {
        private string _expenseName;
        public string ExpenseName 
        {
            get { return _expenseName; }
            set
            {
                _expenseName = value;
                OnPropertyChanged();
            }
        }
        private string _expenseDescription;
        public string ExpenseDescription 
        {
            get { return _expenseDescription; }
            set
            {
                _expenseDescription = value;
                OnPropertyChanged();
            }
        }

        private decimal _expenseAmmount;
        public decimal ExpenseAmmount 
        {
            get { return _expenseAmmount; }
            set
            {
                _expenseAmmount = value;
                OnPropertyChanged();
            }
        }

        private DateTime _expenseDate;
        public DateTime ExpenseDate
        {
            get { return _expenseDate; }
            set
            {
                _expenseDate = value;
                OnPropertyChanged();
            }
        }

        private string _expenseCategory;
        public string ExpenseCategory
        {
            get { return _expenseCategory; }
            set
            {
                _expenseCategory = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<ExpenseStatus> ExpendStatuses { get; set; }


        public NewExpensesViewModel()
        {
            InitializeProperties();
            SaveCommand = new Command(InsertExpense);
            GetCategories();
        }

        private void InitializeProperties()
        {
            Categories = new ObservableCollection<string>();
            ExpendStatuses = new ObservableCollection<ExpenseStatus>();
            ExpenseDate = DateTime.Today;
        }

        private void GetCategories()
        {
            Categories.Clear();
            Categories.Add(Resource.Housing);
            Categories.Add(Resource.Debt);
            Categories.Add(Resource.Health);
            Categories.Add(Resource.Food);
            Categories.Add(Resource.Travel);
            Categories.Add(Resource.Personal);
            Categories.Add(Resource.Other);
        }

        public void InsertExpense() 
        {
            var categoryIndex = 0;
            var expense = new Expense()
            {
                Name = ExpenseName,
                Ammount = ExpenseAmmount,
                Category = int.TryParse(ExpenseCategory, out categoryIndex) ? Categories[categoryIndex] : string.Empty,
                Date = ExpenseDate,
                Description = ExpenseDescription
            };

            var Id = Expense.InsertExpense(expense);

            if (!isDefaultValue(Id)) 
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "No items were inserted", "Ok");
            }

        }

        public void GetExpenseStatuses() 
        {
            ExpendStatuses.Clear();
            ExpendStatuses.Add(new ExpenseStatus()
            {
                Name = "Random",
                Status = true
            });

            ExpendStatuses.Add(new ExpenseStatus()
            {
                Name = "Random 2",
                Status = true
            });

            ExpendStatuses.Add(new ExpenseStatus()
            {
                Name = "Random 3",
                Status = false
            });
        }
    }
}
