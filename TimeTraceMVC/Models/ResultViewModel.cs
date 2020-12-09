using System;

namespace TimeTraceMVC.Models
{
    public class ResultViewModel
    {
        #region Controler
        public ResultViewModel()
        {
            RowNumber = counter;
            counter++;
        }

        #endregion

        #region Fields

        private static int counter = 1;

        #endregion

        #region Properties

        public int RowNumber { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan RaceTime { get; set; }

        #endregion

        #region Methods

        public static void ResetRowCounter()
        {
            counter = 1;
        }

        #endregion
    }
}
