using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;

namespace smartSuite.smartSpriteFX.PictureEngine.Test.AutoPainting.Traces
{
    [TestClass]
    public class AngleManagerTest
    {
        [TestMethod]
        public void ScanFirstPointTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 0,
                        Y = 0
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 10,
                        Y = 10,
                    }
                });

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointSameHypotenusePatternByXYTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 57
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 57,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 274,
                        Y = 80
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 274,
                        Y = 80,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 339,
                        Y = 124
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 339,
                        Y = 124,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717,
                    }
                });

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointSamePatternByXTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 562,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 562,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 956,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 956,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717,
                    }
                });

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointSamePatternByYTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 57
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 57,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 254
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 254,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 522
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 522,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717,
                    }
                });

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(0, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointLPatternCornerATest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 782,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 782,
                        Y = 31,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 300,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 300,
                        Y = 31,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 31,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 156
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 156,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 400
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 400,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 800
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 269,
                        Y = 800,
                    }
                }
                );

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointLPatternCornerBTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 300,
                        Y = 735
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 300,
                        Y = 735,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 700,
                        Y = 735
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 700,
                        Y = 735,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 735
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 735,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 704
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 704,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 400
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 400,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 200
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 200,
                    }
                }
                );

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointLPatternCornerCTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 100
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 100,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 452
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 452,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 700
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 700,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 241,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 280,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 280,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 500,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 500,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 843,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 843,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1187,
                        Y = 717,
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 5187,
                        Y = 717
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 5187,
                        Y = 717,
                    }
                }
                );

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, test.CornerList.Count);

            #endregion
        }

        [TestMethod]
        public void ScanMultiplePointLPatternCornerDTest()
        {
            #region Scenario setup

            #endregion

            #region Running the tested operation

            var test = new AngleManager();
            test.Scan(
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 275,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 275,
                        Y = 31
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 536,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 536,
                        Y = 31
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 31
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 31
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 181
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 181
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 384
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 384
                    }
                },
                new PointHand
                {
                    StartPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 735
                    },
                    EndPoint = new smartSpriteFX.Pictures.Point
                    {
                        X = 1059,
                        Y = 735
                    }
                }
                );

            #endregion

            #region Getting the evidences

            #endregion

            #region Validating the evidences

            Assert.AreEqual(1, test.CornerList.Count);

            #endregion
        }
    }
}
