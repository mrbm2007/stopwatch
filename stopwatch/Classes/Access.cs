using System;
using System.Collections.Generic;
using System.Text;

namespace stopwatch
{
    [Serializable]
    public enum AccessLevel
    {
        /// <summary>
        /// کارشناسان جزء (96 درصدی ها)‏
        /// </summary>
        پژوهشگر
            = 0,
        /// <summary>
        /// فقط مشاهده
        /// </summary>
        مدیر_مرکز
            = 1,
        /// <summary>
        /// ناظر خارج از سازمان
        /// </summary>
        ناظر
            = 2,
        /// <summary>
        /// منشی
        /// </summary>
        منشی
            = 3,
        /// <summary>
        /// مدیران گروه و پروژه
        /// </summary>
        مدیران
            = 4,
        /// <summary>
        /// خانم مرزبان
        /// </summary>
        کنترل
            = 5,
        مدیر_فنی
            = 6,
        /// <summary>
        /// زاغری
        /// </summary>
        نظارت
            = 7,
        /// <summary>
        /// زاغری
        /// </summary>
        کارآموز
            = 8

    }
}
