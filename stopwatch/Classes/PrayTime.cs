using System;
namespace Prayer_Time_Project
{
    public class PrayerTimes
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public DateTime date;

        public DateTime AzanSobh { get; private set; }
        public DateTime Tolue { get; private set; }
        public DateTime AzanZohr { get; private set; }
        public DateTime Ghroub { get; private set; }
        public DateTime AzanMaghreb { get; private set; }
        public DateTime NimeShab { get; private set; }

        public bool UseDayLightSaving { get; set; }

        private const Double PI = System.Math.PI;
        private double XX, YY;
        private DateTime dt;

        public PrayerTimes(double longitude, double latitude, DateTime? date = null, bool useDayLightSaving = true)
        {
            SetGeoAndDate(longitude, latitude, date ?? DateTime.Now.Date, useDayLightSaving);
        }
        public PrayerTimes()
        {
            SetGeoAndDate(stopwatch.Properties.Settings.Default.longitude, stopwatch.Properties.Settings.Default.latitude, DateTime.Now.Date, true);
        }

        public void SetGeoAndDate(double _longitude, double _latitude, DateTime date, bool _useDayLightSaving)
        {
            var pc = new System.Globalization.PersianCalendar();

            this.date = date;
            this.UseDayLightSaving = _useDayLightSaving;
            this.Latitude = _latitude;
            this.Longitude = _longitude;
            this.Year = (ushort)pc.GetYear(date);
            this.Month = (ushort)pc.GetMonth(date);
            this.Day = (ushort)pc.GetDayOfMonth(date);
            dt = new DateTime(this.Year, this.Month, this.Day, new System.Globalization.PersianCalendar());
            SetOwghat();
        }

        private void SetOwghat()
        {
            this.AzanSobh = TimeFormat(GetAzanSobh());
            this.Tolue = TimeFormat(GetTolue());
            SetAzanZohr();
            this.Ghroub = TimeFormat(GetGhoroub());
            this.AzanMaghreb = TimeFormat(GetAzanMaghreb());
            this.NimeShab = TimeFormat(GetNimeShab());
        }

        private void SetAzanZohr()
        {
            Sun(Month, Day, 12, Longitude);
            Sun(Month, Day, XX, Longitude);

            if (this.UseDayLightSaving && TimeZone.CurrentTimeZone.IsDaylightSavingTime(dt))
            {
                XX += TimeZone.CurrentTimeZone.GetDaylightChanges(this.Year).Delta.TotalHours;
            }
            this.AzanZohr = TimeFormat(XX);
        }



        private double GetAzanSobh()
        {
            double delta, ha, t1;
            Sun(Month, Day, 4, Longitude);
            delta = YY;
            ha = Loc2hor(108, delta, Latitude);
            t1 = rRound(XX - ha, 24);
            Sun(Month, Day, t1, Longitude);
            delta = YY;
            ha = Loc2hor(108, delta, Latitude);
            t1 = rRound(XX - ha, 24);
            if (this.UseDayLightSaving && TimeZone.CurrentTimeZone.IsDaylightSavingTime(dt))
            {
                t1 += TimeZone.CurrentTimeZone.GetDaylightChanges(this.Year).Delta.TotalHours;
            }
            return t1;
        }

        private double GetTolue()
        {
            double delta, ha, t2;
            Sun(Month, Day, 6, Longitude);
            delta = YY;
            ha = Loc2hor(90.833, delta, Latitude);
            t2 = rRound(XX - ha, 24);
            Sun(Month, Day, t2, Longitude);
            delta = YY;
            ha = Loc2hor(90.833, delta, Latitude);
            t2 = rRound(XX - ha, 24);
            if (this.UseDayLightSaving && TimeZone.CurrentTimeZone.IsDaylightSavingTime(dt))
            {
                t2 += TimeZone.CurrentTimeZone.GetDaylightChanges(this.Year).Delta.TotalHours;
            }
            return t2;
        }

        private double GetGhoroub()
        {
            double delta, ha, t3;
            Sun(Month, Day, 18, Longitude);
            delta = YY;
            ha = Loc2hor(90.833, delta, Latitude);
            t3 = rRound(XX + ha, 24);
            Sun(Month, Day, t3, Longitude);
            delta = YY;
            ha = Loc2hor(90.833, delta, Latitude);
            t3 = rRound(XX + ha, 24);
            if (this.UseDayLightSaving && TimeZone.CurrentTimeZone.IsDaylightSavingTime(dt))
            {
                t3 += TimeZone.CurrentTimeZone.GetDaylightChanges(this.Year).Delta.TotalHours;
            }
            return t3;
        }

        private double GetAzanMaghreb()
        {
            double delta, ha, t4;
            Sun(Month, Day, 18.5, Longitude);
            delta = YY;
            ha = Loc2hor(94.3, delta, Latitude);
            t4 = rRound(XX + ha, 24);
            Sun(Month, Day, t4, Longitude);
            delta = YY;
            ha = Loc2hor(94.3, delta, Latitude);
            t4 = rRound(XX + ha, 24);
            if (this.UseDayLightSaving && TimeZone.CurrentTimeZone.IsDaylightSavingTime(dt))
            {
                t4 += TimeZone.CurrentTimeZone.GetDaylightChanges(this.Year).Delta.TotalHours;
            }
            return t4;
        }

        private double GetNimeShab()
        {
            double ghroub = GetGhoroub();
            double nightLenght = 24 - ghroub + GetAzanSobh();
            double halfOfNightLenght = nightLenght / 2;
            return (ghroub + halfOfNightLenght);
        }


        private void Sun(double m1, double d1, double h, double lo)
        {
            double m2, l, lst, e, omega, ep, ed, u, v, theta, delta, alpha, ha, zr;
            long i;
            if (m1 < 7)
            {
                d1 = 31 * (m1 - 1) + d1 + h / 24;
            }
            else if (m1 >= 7)
            {
                d1 = 6 + 30 * (m1 - 1) + d1 + h / 24;
            }
            m2 = 74.2023 + 0.98560026 * d1;
            l = -2.75043 + 0.98564735 * d1;
            lst = 8.3162159 + 0.065709824 * Floor(d1) + 1.00273791 * 24 * Mod2(d1, 1) + lo / 15;
            e = 0.0167065;
            omega = 4.85131 - 0.052954 * d1;
            ep = 23.4384717 + 0.00256 * Cosd(omega);
            ed = 180 / PI * e;
            u = m2;
            for (i = 1; i <= 4; i++)
            {
                u = u - (u - ed * Sind(u) - m2) / (1 - e * Cosd(u));
            }
            v = 2 * Atand(Tand(u / 2) * Math.Sqrt((1 + e) / (1 - e)));
            theta = l + v - m2 - 0.00569 - 0.00479 * Sind(omega);
            delta = Asind(Sind(ep) * Sind(theta));

            alpha = 180 / PI * ATan2(Cosd(theta), Cosd(ep) * Sind(theta));

            if (alpha >= 360)
            {
                alpha = alpha - 360;
            }
            ha = lst - alpha / 15;
            zr = rRound(h - ha, 24);

            XX = zr;
            YY = delta;
        }

        private double Loc2hor(double z, double d, double p)
        {
            return Acosd((Cosd(z) - Sind(d) * Sind(p)) / Cosd(d) / Cosd(p)) / 15;
        }

        private double rRound(double X, double a)
        {
            double tmp;
            tmp = Mod2(X, a);
            if (tmp < 0)
            {
                tmp = tmp + a;
            }
            return tmp;
        }

        private DateTime TimeFormat(double X)
        {
            TimeSpan ts = TimeSpan.FromHours(X);
            return date.Date.Add(ts);
        }


        private double Sind(double X)
        {
            return Math.Sin(PI / 180 * X);
        }

        private double Cosd(double X)
        {
            return Math.Cos(PI / 180 * X);
        }

        private double Tand(double X)
        {
            return Math.Tan(PI / 180 * X);
        }

        private double Atand(double X)
        {
            return Math.Atan(X) * 180 / PI;
        }

        private double Asind(double X)
        {
            return Math.Asin(X) * 180 / PI;
        }

        private double Acosd(double X)
        {
            return Math.Acos(X) * 180 / PI;
        }

        private long Floor(double X)
        {
            return Convert.ToInt32(X);
        }

        private double ASin(double X)
        {
            return Math.Atan(X / Math.Sqrt(-X * X + 1.01));
        }

        private double ACos(double X)
        {
            return Math.Atan(-X / Math.Sqrt(-X * X + 1.01)) + 2 * Math.Atan(1);
        }

        private double ATan2(double X, double Y)
        {
            if (X == 0)
            {
                if (Y == 0)
                    return 0;
                else if (Y > 0)
                    return PI / 2;
                else
                    return -PI / 2;
            }
            else if (X > 0)
            {
                if (Y == 0)
                    return 0;
                else
                    return Math.Atan(Y / X);
            }
            else
            {
                if (Y == 0)
                    return PI;
                else
                    return (PI - Math.Atan(Math.Abs(Y) / Math.Abs(X))) * Math.Sign(Y);
            }
        }

        private double Mod2(double a, double b)
        {
            return a - (b * Convert.ToInt32(a / b));
        }

    }
}