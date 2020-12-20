using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalcMine
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int status = 0;
        /* status=0 - начальное состояние, на дисплее -0
         * status=1 - введено первое число 
         * status=2 - введена математическая операция
         * status=3 - введено второе число
         * status=4 - нажата клавиша "Вычислить"
         * status=5 - нажата точка
         * status=6 - набрано число "Пи"       
         */
        private double num1;
        private double num2;
        private double memory;
        private string operation;
        private double result;
        private const double PI = 3.1415926535897931;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public void SelectNumber(object sender, System.EventArgs e)
        {
            //Набираем число
            Button button = (Button)sender;
            string pressed = button.Text;
            
            //Ввод после изначального состояния
            if ( status==0 )
             {
                if (button.Text == ".")
                {
                    this.resulText.Text = "0";
                    status = 1;

                }
                else
                {                  
                    this.resulText.Text = "";
                    status = 1; 
                }
             }

            if ( status == 1)
            {               
                this.resulText.Text += pressed;
               num1 = Convert.ToDouble(this.resulText.Text);
            }

            //Ввод после обозначения математической операции
            if (status == 2)
            {

                this.resulText.Text = "";
                status = 3; 

            }
            if (status==3)
            {
                this.resulText.Text += pressed;
                num2 = Convert.ToDouble(this.resulText.Text);
            }
         
        }

        //Добавляем функцию ввода точки
        public void Comma(object sender, System.EventArgs e) 
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            if (status == 1 )
            {               
                this.resulText.Text += pressed;
                num1 = Convert.ToDouble(this.resulText.Text);              
                status = 5;
                
            }
            if (status == 0)
            { status = 0; }

        }

        //Меняем на противоположное число
        public void Inverted(object sender, System.EventArgs e)
        {
            num1 = -1 * num1;          
            this.resulText.Text = num1.ToString();
            status=1;
        }

        //Выбор операции
        public void SelectOperation(object sender, System.EventArgs e)
        {           
            Button button = (Button)sender;
            string pressed = button.Text;
                  

            if (status == 1 || status ==4 || status ==5) 
            {
                this.resulText.Text = "";
                operation = pressed;
                this.resulText.Text = pressed;
                status = 2;
            }

        }

        // Очистка результата
        public void Clear(object sender, System.EventArgs e)
        {
            
            if (status > 0)
            {
                this.resulText.Text = "0";
                status = 0;
            }
         
        }
  
        //Корректировка ввода
        public void Del(object sender, System.EventArgs e)
        {

            //Корректировка для первого вводимого числа
            if ((status == 1 || status == 4) && resulText.Text.Length > 1)
            {
                resulText.Text = resulText.Text.Substring(0, resulText.Text.Length - 1);
                num1 = Convert.ToDouble(this.resulText.Text);
               
            }

            //Корректировка для второго вводимого числа
            if ((status == 3 ) && resulText.Text.Length > 1) 
            {
                resulText.Text = resulText.Text.Substring(0, resulText.Text.Length - 1);
                num2 = Convert.ToDouble(this.resulText.Text);
               

            }
        }

        //Число Пи
        public void PiConst(object sender, System.EventArgs e) 
        {            
            this.resulText.Text = PI.ToString();         
          
            if (status==0)
            {               
                num1 = PI;                
                status = 5; //Можем редактировать, как число с точкой
            }
        
            if (status==2)
            {
               num2 = PI;              
               status = 6; //Запрещаем редактирование //тут все неочевидно, ведь у нас прекрасно все проходит и по условиям в Del.
                //Проверить логику. Может, убрать редактирование Пи? А играться будем уже с результатом
            }
      
        }

        public void Calculate(object sender, System.EventArgs e)
        {         
            Button button = (Button)sender;
            string pressed = button.Text;
                switch(operation)
            {
                case "+":
                    result = num1 + num2;                   
                    break;

                case "-":
                    result = num1 - num2;
                    break;

                case "/":
                    result = num1/num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "%":
                    result = num1/100;
                    break;               
                case "xʸ":
                    result = Math.Pow(num1, num2);
                    break;
                case "1/x":
                    result = 1 / num1; 
                    break;
                case "SQRT":
                    result = Math.Sqrt(num1);
                    break;
            }

            this.resulText.Text = result.ToString();   
            status = 4;
            num1 = result; //Можем повторить операцию с уже полученным результатом

        }

        //Добавление в память
        public void AddToMemory(object sender, System.EventArgs e)
        {
            memory = Convert.ToDouble(this.resulText.Text);
            this.memoryText.Text = memory.ToString();
        }

        //Добавление в память с отрицательным знаком
        public void NegativeMemory(object sender, System.EventArgs e)
        {

            memory = - Convert.ToDouble(this.resulText.Text); 
            this.memoryText.Text = memory.ToString();
            
        }

        //Чтение из памяти
        public void ReadFromMemory(object sender, System.EventArgs e)
        {
            num1 = memory;
            this.resulText.Text = num1.ToString();
        }

        //Очистка памяти
        public void ClearMemory(object sender, System.EventArgs e)
        {
            memory = 0;
            this.memoryText.Text = "0";
        }
    }

}
