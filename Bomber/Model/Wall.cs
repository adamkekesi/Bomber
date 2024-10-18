﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber.Model
{
    public class Wall : IField
    {
        public void OnCollision(IField otherField, Point point)
        {
            throw new HitAWallException(point);
        }
    }
}
