using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core.Tests
{
    public class SaveRouteCommandTests
    {
        [Test]
        public void Execute_with_null_input_should_throw_error()
        {
            var sut = new SaveRouteCommand(new StreamSaverDummy());
            Assert.Throws<ArgumentNullException>(
                () => sut.Execute(null));
        }

        //[Test]
        //public void Execute_with_ZERO_course_points_gives_no_coursePoints()
        //{
        //    var route = new Route();
            

        //    var sut = new SaveRouteCommand(new StreamSaverDummy());
        //    Assert.Throws<ArgumentNullException>(
        //        () => sut.Execute(null));
        //}
    }

    internal class StreamSaverDummy : IStreamSaver
    {
        public void Save(Stream data)
        {
            // do nothing...
        }
    }
}