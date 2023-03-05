using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace BirthDate
{
    public class BirthDateModelView : INotifyPropertyChanged
    {
        #region Private variables
        private DateTime _birthDate = DateTime.UtcNow.Date;
        private int? _age = null;
        private string _westernSign = null;
        private string _chineseSign = null;
        private bool _dateIsValid = true;
        #endregion

        #region Zodiac signs enums
        public enum WesternZodiacSign
        {
            Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces
        }
        public enum ChineseZodiacSign
        {
            Rat = 1924, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat, Monkey, Rooster, Dog, Pig
        }
        #endregion

        #region Properties
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                BirthDateChanged();
            }
        }
        public string Age => _age == null ? "" : _age.ToString();
        public string WesternSign => _westernSign ?? "";
        public string ChineseSign => _chineseSign ?? "";
        public string Congratulation => CheckIfBirthday(_birthDate) && DateIsValid() ? "Happy birthday!" : "";
        public BitmapImage CongratulationImage => CheckIfBirthday(_birthDate) && DateIsValid() ? new BitmapImage(new Uri("/Images/flowers.png", UriKind.Relative)) : new BitmapImage(new Uri("", UriKind.Relative));
        public string InvalidInputText
        {
            get => _dateIsValid ? "" : "Invalid input!";
            set { }

        }
        #endregion

        #region Functions
        public int ComputeAge(DateTime date)
        {
            TimeSpan age = DateTime.Now - date;
            return age.Days / 365;
        }

        private bool DateIsValid()
        {
            return _birthDate <= DateTime.UtcNow.Date && _age <= 135;
        }

        private bool CheckIfBirthday(DateTime date)
        {
            return date.Day == DateTime.UtcNow.Day && date.Month == DateTime.UtcNow.Month;
        }

        private void BirthDateChanged()
        {
            _age = ComputeAge(_birthDate);

            if (!DateIsValid())
            {
                _dateIsValid = false;
                _age = null;
                _chineseSign = null;
                _westernSign = null;
                InvalidInputText = "Invalid input!";
            }
            else
            {
                _dateIsValid = true;
                _westernSign = ComputeWesternZodiacSign(_birthDate).ToString();
                _chineseSign = ComputeChineseZodiacSign(_birthDate).ToString();
            }

            NotifyPropertyChanged(nameof(Age));
            NotifyPropertyChanged(nameof(Congratulation));
            NotifyPropertyChanged(nameof(CongratulationImage));
            NotifyPropertyChanged(nameof(WesternSign));
            NotifyPropertyChanged(nameof(ChineseSign));
            NotifyPropertyChanged(nameof(InvalidInputText));
        }

        private WesternZodiacSign ComputeWesternZodiacSign(DateTime date)
        {
            if (date < new DateTime(date.Year, 1, 21))
            {
                return WesternZodiacSign.Capricorn;
            }
            if (date < new DateTime(date.Year, 2, 19))
            {
                return WesternZodiacSign.Aquarius;
            }
            if (date < new DateTime(date.Year, 3, 21))
            {
                return WesternZodiacSign.Pisces;
            }
            if (date < new DateTime(date.Year, 4, 21))
            {
                return WesternZodiacSign.Aries;
            }
            if (date < new DateTime(date.Year, 5, 22))
            {
                return WesternZodiacSign.Taurus;
            }
            if (date < new DateTime(date.Year, 6, 22))
            {
                return WesternZodiacSign.Gemini;
            }
            if (date < new DateTime(date.Year, 7, 23))
            {
                return WesternZodiacSign.Cancer;
            }
            if (date < new DateTime(date.Year, 8, 23))
            {
                return WesternZodiacSign.Leo;
            }
            if (date < new DateTime(date.Year, 9, 24))
            {
                return WesternZodiacSign.Virgo;
            }
            if (date < new DateTime(date.Year, 10, 24))
            {
                return WesternZodiacSign.Libra;
            }
            if (date < new DateTime(date.Year, 11, 23))
            {
                return WesternZodiacSign.Scorpio;
            }
            if (date < new DateTime(date.Year, 12, 22))
            {
                return WesternZodiacSign.Sagittarius;
            }
            return WesternZodiacSign.Capricorn;

        }

        private ChineseZodiacSign ComputeChineseZodiacSign(DateTime date)
        {
            int year = date.Year;
            while (year > 1935)
            {
                year -= 12;
            }

            while (year < 1924)
            {
                year += 12;
            }

            return (ChineseZodiacSign)year;
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
