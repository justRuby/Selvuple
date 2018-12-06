using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHelper.Models
{
    public class Composition
    {
        private string _carbohydrates;
        private string _proteins;
        private string _fats;
        private string _calories;

        public string Carbohydrates
        {
            get
            {
                return _carbohydrates;
            }

            set
            {
                _carbohydrates = value;
            }
        }
        public string Proteins
        {
            get
            {
                return _proteins;
            }

            set
            {
                _proteins = value;
            }
        }
        public string Fats
        {
            get
            {
                return _fats;
            }

            set
            {
                _fats = value;
            }
        }
        public string Calories
        {
            get
            {
                return _calories;
            }

            set
            {
                _calories = value;
            }
        }

        public Composition()
        {

        }

        public bool Check()
        {
            throw new Exception();
        }

        public string GetResult()
        {
            throw new Exception();
        }
    }
}
