using System;
using System.Collections.Generic;

namespace Events_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<Customer> customers = new Queue<Customer>();
            customers.Enqueue(new Customer { FirstName = "Sally", LastName = "Jones" }); ; ; ; ; ; ; ; ;
            customers.Enqueue(new Customer { FirstName = "David", LastName = "Silver" }); ; ; ; ; ;
            customers.Enqueue(new Customer { FirstName = "Kevin", LastName = "Debruyne" });
            customers.Enqueue(new Customer { FirstName = "Eden", LastName = "Harzard" });
            customers.Enqueue(new Customer { FirstName = "Cristian", LastName = "Pulisic" });

          
            openTable newTable = new openTable();

            foreach (Customer customer in customers)
            {

                var open = new openTable();
                open.openTableEvent += customer.OnOpenTable;

                open.Table();

                Meals meals = Meals.none;

                for (int i = 0; i <= 3; i++)
                {
                    meals += 1;

                    customer.mealChangeEvent += customer.HandleMealChange;
                    customer.HandleMealChange(customer, new eventarg(customer.FirstName, customer.LastName, meals));
                }
            }
            
        }

    }

    public class eventarg : EventArgs
    {
        public string FN { get; set; }
        public string LN { get; set; }
        public Meals meals { get; set; }
        public eventarg(string FirstName, string LastName, Meals meals)
        {
            this.FN = FirstName;
            this.LN = LastName;
            this.meals = meals;

        }
    }
    public class openTable
    {

        public delegate void openTableEventHandler(object source, EventArgs e);
        public event openTableEventHandler openTableEvent;

        public void Table()
        {
            Console.WriteLine("\n\n\nTable is Open!");
            openTableEventHandler table = openTableEvent;
            if (table != null)
            {
                table(this, new EventArgs());
            }
        }
    }

    public enum Meals { none, appetizer, main, desert, done }
    public delegate void mealChangeEventHandler(object source, eventarg e);
    public class Customer
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        
        public void OnOpenTable(object source, EventArgs e)
        {
            Console.WriteLine("{0} {1} got a table.", this.FirstName, this.LastName);
        }
       
        
        public  event mealChangeEventHandler mealChangeEvent;

        public  void HandleMealChange(object sender, eventarg e)
        {
            Console.WriteLine("\n{0} {1} is having {2}.", e.FN, e.LN, e.meals);
            mealChangeEventHandler newMeal = mealChangeEvent;
            /*if (mealChangeEvent != null)
            {
                newMeal(this, new eventarg(e.FN, e.LN, e.meals));
            }*/
        }
    }
      
}


