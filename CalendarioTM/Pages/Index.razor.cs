using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using static CalendarioTM.Pages.Extensions;

namespace CalendarioTM.Pages
{
    public partial class Index
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        private string _abnormalBloomString => $"[fioritura anomala: {Extensions._abnormalBloom:dd/MM/yyyy}]";

        private string _error;

        private int _day = DateTime.Now.Day;
        private int _month = DateTime.Now.Month;
        private int _year = DateTime.Now.Year;
        private string _gregorianString;

        private int _dayIC;
        private int _monthIC;
        private int _yearIC;
        private string _imperialString;

        private int _dayE;
        private int _monthE;
        private int _partE;
        private int _bloomE;
        private string _quenyaString;
        private string _sindarString;

        protected override void OnInitialized()
        {
            FromGregorian();
        }

        private void SetError(string text)
        {
            _error = text;
            _gregorianString = string.Empty;
            _imperialString = string.Empty;
            _quenyaString = string.Empty;
            _sindarString = string.Empty;
        }

        private void FromGregorian()
        {
            if (DateTime.TryParse($"{_year}-{_month}-{_day}", out DateTime gregorianDate))
            {
                _error = string.Empty;
                (_yearIC, _monthIC, _dayIC) = gregorianDate.ToImperialCount();
                (_bloomE, _partE, _monthE, _dayE) = gregorianDate.ToElvenDate();

                UpdateGregorianString();
                UpdateImperialString();
                UpdateQuenyaString();
                UpdateSindarString();
            }
            else
            {
                SetError("Data non valida");
            }
        }

        private void FromImperial()
        {
            _day = _dayIC;
            _month = _monthIC;
            _year = _yearIC + 1736;

            FromGregorian();
        }

        private void FromElven()
        {
            if (elvenDateDoesntExist())
            {
                SetError("Data elfica inesistente per via della fioritura anomala");
            }
            else
            {
                DateTime date = FromElvenToGregorian(_bloomE, _partE, _monthE, _dayE);
                if (InputWasGood(date))
                {
                    _error = string.Empty;
                    _year = date.Year;
                    _month = date.Month;
                    _day = date.Day;
                    FromGregorian();
                }
            }

            // ↓↓ local functions ↓↓ //
            bool elvenDateDoesntExist()
            {
                return _bloomE == 21 && _partE >= 39 && _monthE >= 13 && _dayE >= 5;
            }

            bool InputWasGood(DateTime date)
            {
                if (_monthE == 13 && _dayE > 6)
                {
                    SetError("Data elfica non valida");
                    return false;
                }
                else if (_monthE == 13 && _dayE == 6 && date.Day != 31)
                {
                    SetError("Data elfica non bisestile");
                    return false;
                }
                return true;
            }
        }

        private DateTime FromElvenToGregorian(int bloom, int part, int month, int day)
        {
            int year = getYear();
            int dayOfYear = (month - 1) * 30 + day;
            return new DateTime(year, 1, 1).AddDays(dayOfYear - 1);

            // ↓↓ local functions ↓↓ //
            int getYear()
            {
                if (bloom >= 22)
                { return bloom * 78 + part + 297; }
                else
                { return bloom * 78 + part + 336; }
            }
        }

        private void UpdateGregorianString()
        {
            _gregorianString = $"{_day} {GregorianMonths[_month]} {_year}";
        }

        private void UpdateImperialString()
        {
            _imperialString = $"{_dayIC} {ImperialMonths[_monthIC]} {_yearIC}";
        }

        private void UpdateQuenyaString()
        {
            _quenyaString = $"{_dayE} {QuenyaMonths[_monthE]} {_partE}^ parte della {_bloomE}^ Fioritura";
        }

        private void UpdateSindarString()
        {
            _sindarString = $"{_dayE} {SindarMonths[_monthE]} {_partE}^ parte della {_bloomE}^ Fioritura";
        }

        private void CopyGregorianToClipboard() => CopyToClipboard(_gregorianString);
        private void CopyImperialToClipboard() => CopyToClipboard(_imperialString);
        private void CopyQuenyaToClipboard() => CopyToClipboard(_quenyaString);
        private void CopySindarToClipboard() => CopyToClipboard(_sindarString);

        private void CopyToClipboard(string text)
        {
            JSRuntime.InvokeVoidAsync("clipboardCopy.copyText", text);
        }

        private void FromGregorianKeyup(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
                FromGregorian();
        }

        private void FromImperialKeyup(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
                FromImperial();
        }

        private void FromElvenKeyup(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
                FromElven();
        }
    }
}
