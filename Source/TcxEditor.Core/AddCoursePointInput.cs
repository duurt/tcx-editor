﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddCoursePointInput : IInput
    {
        public Route Route { get; set; }
        public CoursePoint NewCoursePoint { get; set; }
    }
}