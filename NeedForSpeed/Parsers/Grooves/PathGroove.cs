﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PlatformEngine;

namespace Carmageddon.Parsers.Grooves
{
    class PathGroove : BaseGroove
    {
        public Motion Motion;
        public Vector3 Movement;
        float _currentPos;
        float _direction = 1;
        float _speed2 = 2;

        public Vector3 UpdateMovement()
        {
            switch (Motion)
            {
                case Motion.Harmonic:
                //_currentPos += _direction * Engine.Instance.ElapsedSeconds * Speed * Movement * _speed2;

                //float distance = Vector3.Distance(Movement, _currentPos);
                //if (distance < 0.15f)
                //    _speed2 = MathHelper.Lerp(0.05f, 1f, distance);
                //else
                //    _speed2 = 1;
                //break;
                case Motion.Linear:
                    _currentPos += _direction * Engine.Instance.ElapsedSeconds * Speed;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (_currentPos > 1)
            {
                _currentPos = 1;
                _direction *= -1;
            }
            if (_currentPos <= 0)
            {
                _currentPos = 0;
                _direction *= -1;
            }

           return Vector3.Lerp(-Movement, Movement, _currentPos);
        }

        public override void Update()
        {
            _actor.Matrix =
                _scale * _actorRotation
                * _translation
                * Matrix.CreateTranslation(UpdateMovement());
        }
    }
}