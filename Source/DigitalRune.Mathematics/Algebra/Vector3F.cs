// DigitalRune Engine - Copyright (C) DigitalRune GmbH
// This file is subject to the terms and conditions defined in
// file 'LICENSE.TXT', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
#if !NETFX_CORE && !PORTABLE
using DigitalRune.Mathematics.Algebra.Design;
#endif
#if XNA || MONOGAME
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
#endif


namespace DigitalRune.Mathematics.Algebra
{
  /// <summary>
  /// Defines a 3-dimensional vector (single-precision).
  /// </summary>
  /// <remarks>
  /// The three components (x, y, z) are stored with single-precision.
  /// </remarks>
#if !NETFX_CORE && !SILVERLIGHT && !WP7 && !WP8 && !XBOX && !UNITY && !PORTABLE
  [Serializable]
#endif
#if !NETFX_CORE && !PORTABLE
  [TypeConverter(typeof(Vector3FConverter))]
#endif
#if !XBOX && !UNITY
  [DataContract]
#endif
  public struct Vector3F : IEquatable<Vector3F>
  {
    //--------------------------------------------------------------
    #region Constants
    //--------------------------------------------------------------

    /// <summary>
    /// Returns a <see cref="Vector3F"/> with all of its components set to zero.
    /// </summary>
    public static readonly Vector3F Zero = new Vector3F(0, 0, 0);


    /// <summary>
    /// Returns a <see cref="Vector3F"/> with all of its components set to one.
    /// </summary>
    public static readonly Vector3F One = new Vector3F(1, 1, 1);


    /// <summary>
    /// Returns the x unit <see cref="Vector3F"/> (1, 0, 0).
    /// </summary>
    public static readonly Vector3F UnitX = new Vector3F(1, 0, 0);


    /// <summary>
    /// Returns the value2 unit <see cref="Vector3F"/> (0, 1, 0).
    /// </summary>
    public static readonly Vector3F UnitY = new Vector3F(0, 1, 0);


    /// <summary>
    /// Returns the z unit <see cref="Vector3F"/> (0, 0, 1).
    /// </summary>
    public static readonly Vector3F UnitZ = new Vector3F(0, 0, 1);


#if XNA || MONOGAME
    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing forward (0, 0, −1).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Forward = new Vector3F(0, 0, -1);


    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing backward (0, 0, 1).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Backward = new Vector3F(0, 0, 1);


    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing left (-1, 0, 0).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Left = new Vector3F(-1, 0, 0);


    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing right (1, 0, 0).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Right = new Vector3F(1, 0, 0);


    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing up (0, 1, 0).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Up = new Vector3F(0, 1, 0);


    /// <summary>
    /// Returns a unit <see cref="Vector3F"/> pointing down (0, −1, 0).
    /// (Only available in the XNA-compatible build.)
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </para>
    /// <para>
    /// <strong>DigitalRune</strong> uses the same coordinate systems as the
    /// <strong>XNA Framework:</strong> model space (object space, local space), world space 
    /// and view space are right-handed coordinate systems where, by default, the positive 
    /// x-axis points to the right, the positive y-axis points up, and the positive z-axis 
    /// points towards the viewer.
    /// </para>
    /// </remarks>
    public static readonly Vector3F Down = new Vector3F(0, -1, 0);
#endif
    #endregion


    //--------------------------------------------------------------
    #region Fields
    //--------------------------------------------------------------

    /// <summary>
    /// The x component.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
#if !XBOX && !UNITY
    [DataMember]
#endif
    public float X;

    /// <summary>
    /// The y component.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
#if !XBOX && !UNITY
    [DataMember]
#endif
    public float Y;

    /// <summary>
    /// The z component.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
#if !XBOX && !UNITY
    [DataMember]
#endif
    public float Z;
    #endregion


    //--------------------------------------------------------------
    #region Properties
    //--------------------------------------------------------------

    /// <summary>
    /// Gets or sets the component at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <value>The component at <paramref name="index"/>.</value>
    /// <remarks>
    /// The index is zero based: x = vector[0], y = vector[1], z = vector[2].
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="index"/> is out of range.
    /// </exception>
    public float this[int index]
    {
      get
      {
        switch (index)
        {
          case 0: return X;
          case 1: return Y;
          case 2: return Z;
          default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, or 2.");
        }
      }
      set
      {
        switch (index)
        {
          case 0: X = value; break;
          case 1: Y = value; break;
          case 2: Z = value; break;
          default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, or 2.");
        }
      }
    }


    /// <summary>
    /// Gets a value indicating whether a component of the vector is <see cref="float.NaN"/>.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if a component of the vector is <see cref="float.NaN"/>; otherwise, 
    /// <see langword="false"/>.
    /// </value>
    public bool IsNaN
    {
      get { return Numeric.IsNaN(X) || Numeric.IsNaN(Y) || Numeric.IsNaN(Z); }
    }


    /// <summary>
    /// Returns a value indicating whether this vector is normalized (the length is numerically
    /// equal to 1).
    /// </summary>
    /// <value>
    /// <see langword="true"/> if this <see cref="Vector3F"/> is normalized; otherwise, 
    /// <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// <see cref="IsNumericallyNormalized"/> compares the length of this vector against 1.0 using
    /// the default tolerance value (see <see cref="Numeric.EpsilonF"/>).
    /// </remarks>
    public bool IsNumericallyNormalized
    {
      get { return Numeric.AreEqual(LengthSquared, 1.0f); }
    }


    /// <summary>
    /// Returns a value indicating whether this vector has zero size (the length is numerically
    /// equal to 0).
    /// </summary>
    /// <value>
    /// <see langword="true"/> if this vector is numerically zero; otherwise, 
    /// <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// The length of this vector is compared to 0 using the default tolerance value (see 
    /// <see cref="Numeric.EpsilonF"/>).
    /// </remarks>
    public bool IsNumericallyZero
    {
      get { return Numeric.IsZero(LengthSquared, Numeric.EpsilonFSquared); }
    }


    /// <summary>
    /// Gets or sets the length of this vector.
    /// </summary>
    /// <returns>The length of the this vector.</returns>
    /// <exception cref="MathematicsException">
    /// The vector has a length of 0. The length cannot be changed.
    /// </exception>
    [XmlIgnore]
#if XNA || MONOGAME
    [ContentSerializerIgnore]
#endif
    public float Length
    {
      get
      {
        return (float) Math.Sqrt(X * X + Y * Y + Z * Z);
      }
      set
      {
        float length = Length;
        if (Numeric.IsZero(length))
          throw new MathematicsException("Cannot change length of a vector with length 0.");

        float scale = value / length;
        X *= scale;
        Y *= scale;
        Z *= scale;
      }
    }


    /// <summary>
    /// Returns the squared length of this vector.
    /// </summary>
    /// <returns>The squared length of this vector.</returns>
    public float LengthSquared
    {
      get
      {
        return X * X + Y * Y + Z * Z;
      }
    }


    /// <summary>
    /// Returns the normalized vector.
    /// </summary>
    /// <value>The normalized vector.</value>
    /// <remarks>
    /// The property does not change this instance. To normalize this instance you need to call 
    /// <see cref="Normalize"/>.
    /// </remarks>
    /// <exception cref="DivideByZeroException">
    /// The length of the vector is zero. The quaternion cannot be normalized.
    /// </exception>
    public Vector3F Normalized
    {
      get
      {
        Vector3F v = this;
        v.Normalize();
        return v;
      }
    }


    /// <summary>
    /// Returns an arbitrary normalized <see cref="Vector3F"/> that is orthogonal to this vector.
    /// </summary>
    /// <value>An arbitrary normalized orthogonal <see cref="Vector3F"/>.</value>
    public Vector3F Orthonormal1
    {
      // Note: Other options to create normal vectors are discussed here:
      // http://blog.selfshadow.com/2011/10/17/perp-vectors/,
      // http://box2d.org/2014/02/computing-a-basis/
      // and here
      // "Building an Orthonormal Basis from a 3D Unit Vector Without Normalization"
      // http://orbit.dtu.dk/fedora/objects/orbit:113874/datastreams/file_75b66578-222e-4c7d-abdf-f7e255100209/content
      // This method is implemented in DigitalRune.Graphics/Misc.fxh/GetOrthonormals().

      get
      {
        Vector3F v;
        if (Numeric.IsZero(Z) == false)
        {
          // Orthonormal = (1, 0, 0) x (X, Y, Z)
          v.X = 0f;
          v.Y = -Z;
          v.Z = Y;
        }
        else
        {
          // Orthonormal = (0, 0, 1) x (X, Y, Z)
          v.X = -Y;
          v.Y = X;
          v.Z = 0f;
        }
        v.Normalize();
        return v;
      }
    }


    /// <summary>
    /// Gets a normalized orthogonal <see cref="Vector3F"/> that is orthogonal to this 
    /// <see cref="Vector3F"/> and to <see cref="Orthonormal1"/>.
    /// </summary>
    /// <value>
    /// A normalized orthogonal <see cref="Vector3F"/> which is orthogonal to this 
    /// <see cref="Vector3F"/> and to <see cref="Orthonormal1"/>.
    /// </value>
    public Vector3F Orthonormal2
    {
      get
      {
        Vector3F v = Cross(this, Orthonormal1);
        v.Normalize();
        return v;
      }
    }


    /// <summary>
    /// Gets the value of the largest component.
    /// </summary>
    /// <value>The value of the largest component.</value>
    public float LargestComponent
    {
      get
      {
        if (X >= Y && X >= Z)
          return X;

        if (Y >= Z)
          return Y;

        return Z;
      }
    }


    /// <summary>
    /// Gets the index (zero-based) of the largest component.
    /// </summary>
    /// <value>The index (zero-based) of the largest component.</value>
    /// <remarks>
    /// <para>
    /// This method returns the index of the component (X, Y or Z) which has the largest value. The 
    /// index is zero-based, i.e. the index of X is 0. 
    /// </para>
    /// <para>
    /// If there are several components with equally large values, the smallest index of these is 
    /// returned.
    /// </para>
    /// </remarks>
    public int IndexOfLargestComponent
    {
      get
      {
        if (X >= Y && X >= Z)
          return 0;

        if (Y >= Z)
          return 1;

        return 2;
      }
    }


    /// <summary>
    /// Gets the value of the smallest component.
    /// </summary>
    /// <value>The value of the smallest component.</value>
    public float SmallestComponent
    {
      get
      {
        if (X <= Y && X <= Z)
          return X;

        if (Y <= Z)
          return Y;

        return Z;
      }
    }


    /// <summary>
    /// Gets the index (zero-based) of the largest component.
    /// </summary>
    /// <value>The index (zero-based) of the largest component.</value>
    /// <remarks>
    /// <para>
    /// This method returns the index of the component (X, Y or Z) which has the smallest value. The 
    /// index is zero-based, i.e. the index of X is 0. 
    /// </para>
    /// <para>
    /// If there are several components with equally small values, the smallest index of these is 
    /// returned.
    /// </para>
    /// </remarks>
    public int IndexOfSmallestComponent
    {
      get
      {
        if (X <= Y && X <= Z)
          return 0;

        if (Y <= Z)
          return 1;

        return 2;
      }
    }
    #endregion


    //--------------------------------------------------------------
    #region Creation & Cleanup
    //--------------------------------------------------------------

    /// <overloads>
    /// <summary>
    /// Initializes a new instance of <see cref="Vector3F"/>.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Initializes a new instance of <see cref="Vector3F"/>.
    /// </summary>
    /// <param name="x">Initial value for the x component.</param>
    /// <param name="y">Initial value for the y component.</param>
    /// <param name="z">Initial value for the z component.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public Vector3F(float x, float y, float z)
    {
      X = x;
      Y = y;
      Z = z;
    }


    /// <summary>
    /// Initializes a new instance of <see cref="Vector3F"/>.
    /// </summary>
    /// <param name="componentValue">The initial value for 3 the vector components.</param>
    /// <remarks>
    /// All components are set to <paramref name="componentValue"/>.
    /// </remarks>
    public Vector3F(float componentValue)
    {
      X = componentValue;
      Y = componentValue;
      Z = componentValue;
    }


    /// <summary>
    /// Initializes a new instance of <see cref="Vector3F"/>.
    /// </summary>
    /// <param name="components">
    /// Array with the initial values for the components x, y and z.
    /// </param>
    /// <exception cref="IndexOutOfRangeException">
    /// <paramref name="components"/> has less than 3 elements.
    /// </exception>
    /// <exception cref="NullReferenceException">
    /// <paramref name="components"/> must not be <see langword="null"/>.
    /// </exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods")]
    public Vector3F(float[] components)
    {
      X = components[0];
      Y = components[1];
      Z = components[2];
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="Vector3F"/> class.
    /// </summary>
    /// <param name="components">
    /// List with the initial values for the components x, y and z.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="components"/> has less than 3 elements.
    /// </exception>
    /// <exception cref="NullReferenceException">
    /// <paramref name="components"/> must not be <see langword="null"/>.
    /// </exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods")]
    public Vector3F(IList<float> components)
    {
      X = components[0];
      Y = components[1];
      Z = components[2];
    }
    #endregion


    //--------------------------------------------------------------
    #region Interfaces and Overrides
    //--------------------------------------------------------------

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode()
    {
      // ReSharper disable NonReadonlyFieldInGetHashCode
      unchecked
      {
        int hashCode = X.GetHashCode();
        hashCode = (hashCode * 397) ^ Y.GetHashCode();
        hashCode = (hashCode * 397) ^ Z.GetHashCode();
        return hashCode;
      }
      // ReSharper restore NonReadonlyFieldInGetHashCode
    }


    /// <overloads>
    /// <summary>
    /// Indicates whether a vector and a another object are equal.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">Another object to compare to.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> and this instance are the same type and
    /// represent the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj)
    {
      return obj is Vector3F && this == (Vector3F)obj;
    }


    #region IEquatable<Vector3F> Members
    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true"/> if the current object is equal to the other parameter; otherwise, 
    /// <see langword="false"/>.
    /// </returns>
    public bool Equals(Vector3F other)
    {
      return this == other;
    }
    #endregion


    /// <overloads>
    /// <summary>
    /// Returns the string representation of a vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Returns the string representation of this vector.
    /// </summary>
    /// <returns>The string representation of this vector.</returns>
    public override string ToString()
    {
      return ToString(CultureInfo.CurrentCulture);
    }


    /// <summary>
    /// Returns the string representation of this vector using the specified culture-specific format
    /// information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.
    /// </param>
    /// <returns>The string representation of this vector.</returns>
    public string ToString(IFormatProvider provider)
    {
      return string.Format(provider, "({0}; {1}; {2})", X, Y, Z);
    }
    #endregion


    //--------------------------------------------------------------
    #region Overloaded Operators
    //--------------------------------------------------------------

    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector3F operator -(Vector3F vector)
    {
      vector.X = -vector.X;
      vector.Y = -vector.Y;
      vector.Z = -vector.Z;
      return vector;
    }


    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector3F Negate(Vector3F vector)
    {
      vector.X = -vector.X;
      vector.Y = -vector.Y;
      vector.Z = -vector.Z;
      return vector;
    }


    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The sum of the two vectors.</returns>
    public static Vector3F operator +(Vector3F vector1, Vector3F vector2)
    {
      vector1.X += vector2.X;
      vector1.Y += vector2.Y;
      vector1.Z += vector2.Z;
      return vector1;
    }


    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The sum of the two vectors.</returns>
    public static Vector3F Add(Vector3F vector1, Vector3F vector2)
    {
      vector1.X += vector2.X;
      vector1.Y += vector2.Y;
      vector1.Z += vector2.Z;
      return vector1;
    }


    /// <summary>
    /// Subtracts a vector from a vector.
    /// </summary>
    /// <param name="minuend">The first vector (minuend).</param>
    /// <param name="subtrahend">The second vector (subtrahend).</param>
    /// <returns>The difference of the two vectors.</returns>
    public static Vector3F operator -(Vector3F minuend, Vector3F subtrahend)
    {
      minuend.X -= subtrahend.X;
      minuend.Y -= subtrahend.Y;
      minuend.Z -= subtrahend.Z;
      return minuend;
    }


    /// <summary>
    /// Subtracts a vector from a vector.
    /// </summary>
    /// <param name="minuend">The first vector (minuend).</param>
    /// <param name="subtrahend">The second vector (subtrahend).</param>
    /// <returns>The difference of the two vectors.</returns>
    public static Vector3F Subtract(Vector3F minuend, Vector3F subtrahend)
    {
      minuend.X -= subtrahend.X;
      minuend.Y -= subtrahend.Y;
      minuend.Z -= subtrahend.Z;
      return minuend;
    }


    /// <overloads>
    /// <summary>
    /// Multiplies a vector by a scalar or a vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns>The vector with each component multiplied by <paramref name="scalar"/>.</returns>
    public static Vector3F operator *(Vector3F vector, float scalar)
    {
      vector.X *= scalar;
      vector.Y *= scalar;
      vector.Z *= scalar;
      return vector;
    }


    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns>The vector with each component multiplied by <paramref name="scalar"/>.</returns>
    public static Vector3F operator *(float scalar, Vector3F vector)
    {
      vector.X *= scalar;
      vector.Y *= scalar;
      vector.Z *= scalar;
      return vector;
    }


    /// <overloads>
    /// <summary>
    /// Multiplies a vector by a scalar or a vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns>The vector with each component multiplied by <paramref name="scalar"/>.</returns>
    public static Vector3F Multiply(float scalar, Vector3F vector)
    {
      vector.X *= scalar;
      vector.Y *= scalar;
      vector.Z *= scalar;
      return vector;
    }


    /// <summary>
    /// Multiplies the components of two vectors by each other.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise product of the two vectors.</returns>
    public static Vector3F operator *(Vector3F vector1, Vector3F vector2)
    {
      vector1.X *= vector2.X;
      vector1.Y *= vector2.Y;
      vector1.Z *= vector2.Z;
      return vector1;
    }


    /// <summary>
    /// Multiplies the components of two vectors by each other.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The component-wise product of the two vectors.</returns>
    public static Vector3F Multiply(Vector3F vector1, Vector3F vector2)
    {
      vector1.X *= vector2.X;
      vector1.Y *= vector2.Y;
      vector1.Z *= vector2.Z;
      return vector1;
    }


    /// <overloads>
    /// <summary>
    /// Divides the vector by a scalar or a vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns>The vector with each component divided by <paramref name="scalar"/>.</returns>
    public static Vector3F operator /(Vector3F vector, float scalar)
    {
      float f = 1 / scalar;
      vector.X *= f;
      vector.Y *= f;
      vector.Z *= f;
      return vector;
    }


    /// <overloads>
    /// <summary>
    /// Divides the vector by a scalar or a vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns>The vector with each component divided by <paramref name="scalar"/>.</returns>
    public static Vector3F Divide(Vector3F vector, float scalar)
    {
      float f = 1 / scalar;
      vector.X *= f;
      vector.Y *= f;
      vector.Z *= f;
      return vector;
    }


    /// <summary>
    /// Divides the components of a vector by the components of another vector.
    /// </summary>
    /// <param name="dividend">The first vector (dividend).</param>
    /// <param name="divisor">The second vector (divisor).</param>
    /// <returns>The component-wise product of the two vectors.</returns>
    public static Vector3F operator /(Vector3F dividend, Vector3F divisor)
    {
      dividend.X /= divisor.X;
      dividend.Y /= divisor.Y;
      dividend.Z /= divisor.Z;
      return dividend;
    }


    /// <summary>
    /// Divides the components of a vector by the components of another vector.
    /// </summary>
    /// <param name="dividend">The first vector (dividend).</param>
    /// <param name="divisor">The second vector (divisor).</param>
    /// <returns>The component-wise division of the two vectors.</returns>
    public static Vector3F Divide(Vector3F dividend, Vector3F divisor)
    {
      dividend.X /= divisor.X;
      dividend.Y /= divisor.Y;
      dividend.Z /= divisor.Z;
      return dividend;
    }


    /// <summary>
    /// Tests if two vectors are equal.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if the vectors are equal; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// For the test the corresponding components of the vectors are compared.
    /// </remarks>
    public static bool operator ==(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X == vector2.X
          && vector1.Y == vector2.Y
          && vector1.Z == vector2.Z;
    }


    /// <summary>
    /// Tests if two vectors are not equal.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if the vectors are different; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// For the test the corresponding components of the vectors are compared.
    /// </remarks>
    public static bool operator !=(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X != vector2.X
          || vector1.Y != vector2.Y
          || vector1.Z != vector2.Z;
    }


    /// <summary>
    /// Tests if each component of a vector is greater than the corresponding component of another
    /// vector.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if each component of <paramref name="vector1"/> is greater than its
    /// counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    public static bool operator >(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X > vector2.X
          && vector1.Y > vector2.Y
          && vector1.Z > vector2.Z;
    }


    /// <summary>
    /// Tests if each component of a vector is greater or equal than the corresponding component of
    /// another vector.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if each component of <paramref name="vector1"/> is greater or equal
    /// than its counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    public static bool operator >=(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X >= vector2.X
          && vector1.Y >= vector2.Y
          && vector1.Z >= vector2.Z;
    }


    /// <summary>
    /// Tests if each component of a vector is less than the corresponding component of another
    /// vector.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if each component of <paramref name="vector1"/> is less than its 
    /// counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    public static bool operator <(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X < vector2.X
          && vector1.Y < vector2.Y
          && vector1.Z < vector2.Z;
    }


    /// <summary>
    /// Tests if each component of a vector is less or equal than the corresponding component of
    /// another vector.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if each component of <paramref name="vector1"/> is less or equal than
    /// its counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
    public static bool operator <=(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X <= vector2.X
          && vector1.Y <= vector2.Y
          && vector1.Z <= vector2.Z;
    }


    /// <overloads>
    /// <summary>
    /// Converts a vector to another data type.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Converts a vector to an array of 3 <see langword="float"/> values.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>
    /// The array with 3 <see langword="float"/> values. The order of the elements is: x, y, z
    /// </returns>
    public static explicit operator float[](Vector3F vector)
    {
      return new[] { vector.X, vector.Y, vector.Z };
    }


    /// <summary>
    /// Converts this vector to an array of 3 <see langword="float"/> values.
    /// </summary>
    /// <returns>
    /// The array with 3 <see langword="float"/> values. The order of the elements is: x, y, z
    /// </returns>
    public float[] ToArray()
    {
      return (float[]) this;
    }


    /// <summary>
    /// Converts a vector to a list of 3 <see langword="float"/> values.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The result of the conversion. The order of the elements is: x, y, z</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
    public static explicit operator List<float>(Vector3F vector)
    {
      List<float> result = new List<float>(3) { vector.X, vector.Y, vector.Z };
      return result;
    }


    /// <summary>
    /// Converts this vector to a list of 3 <see langword="float"/> values.
    /// </summary>
    /// <returns>The result of the conversion. The order of the elements is: x, y, z</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
    public List<float> ToList()
    {
      return (List<float>) this;
    }


    /// <overloads>
    /// <summary>
    /// Performs an implicit conversion from <see cref="Vector3F"/> to another data type.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Performs an implicit conversion from <see cref="Vector3F"/> to <see cref="Vector3D"/>.
    /// </summary>
    /// <param name="vector">The DigitalRune <see cref="Vector3F"/>.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector3D(Vector3F vector)
    {
      return new Vector3D(vector.X, vector.Y, vector.Z);
    }


    /// <summary>
    /// Converts this <see cref="Vector3F"/> to <see cref="Vector3D"/>.
    /// </summary>
    /// <returns>The result of the conversion.</returns>
    public Vector3D ToVector3D()
    {
      return new Vector3D(X, Y, Z);
    }


    /// <summary>
    /// Performs an implicit conversion from <see cref="Vector3F"/> to <see cref="VectorF"/>.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator VectorF(Vector3F vector)
    {
      VectorF result = new VectorF(3);
      result[0] = vector.X; result[1] = vector.Y; result[2] = vector.Z;
      return result;
    }


    /// <summary>
    /// Converts this <see cref="Vector3F"/> to <see cref="VectorF"/>.
    /// </summary>
    /// <returns>The result of the conversion.</returns>
    public VectorF ToVectorF()
    {
      return this;
    }


#if XNA || MONOGAME
    /// <summary>
    /// Performs an conversion from <see cref="Vector3"/> (XNA Framework) to <see cref="Vector3F"/>
    /// (DigitalRune Mathematics).
    /// </summary>
    /// <param name="vector">The <see cref="Vector3"/> (XNA Framework).</param>
    /// <returns>The <see cref="Vector3F"/> (DigitalRune Mathematics).</returns>
    /// <remarks>
    /// This method is available only in the XNA-compatible build of the
    /// DigitalRune.Mathematics.dll.
    /// </remarks>
    public static explicit operator Vector3F(Vector3 vector)
    {
      return new Vector3F(vector.X, vector.Y, vector.Z);
    }


    /// <summary>
    /// Converts this <see cref="Vector3F"/> (DigitalRune Mathematics) to <see cref="Vector3"/> 
    /// (XNA Framework).
    /// </summary>
    /// <param name="vector">The <see cref="Vector3"/> (XNA Framework).</param>
    /// <returns>The <see cref="Vector3F"/> (DigitalRune Mathematics).</returns>
    /// <remarks>
    /// This method is available only in the XNA-compatible build of the 
    /// DigitalRune.Mathematics.dll.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public static Vector3F FromXna(Vector3 vector)
    {
      return new Vector3F(vector.X, vector.Y, vector.Z);
    }


    /// <summary>
    /// Performs an conversion from <see cref="Vector3F"/> (DigitalRune Mathematics) to 
    /// <see cref="Vector3"/> (XNA Framework).
    /// </summary>
    /// <param name="vector">The <see cref="Vector3F"/> (DigitalRune Mathematics).</param>
    /// <returns>The <see cref="Vector3"/> (XNA Framework).</returns>
    /// <remarks>
    /// This method is available only in the XNA-compatible build of the
    /// DigitalRune.Mathematics.dll.
    /// </remarks>
    public static explicit operator Vector3(Vector3F vector)
    {
      return new Vector3(vector.X, vector.Y, vector.Z);
    }


    /// <summary>
    /// Converts this <see cref="Vector3F"/> (DigitalRune Mathematics) to <see cref="Vector3"/> 
    /// (XNA Framework).
    /// </summary>
    /// <returns>The <see cref="Vector3"/> (XNA Framework).</returns>
    /// <remarks>
    /// This method is available only in the XNA-compatible build of the DigitalRune.Mathematics.dll.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public Vector3 ToXna()
    {
      return new Vector3(X, Y, Z);
    }
#endif
    #endregion


    //--------------------------------------------------------------
    #region Methods
    //--------------------------------------------------------------

    /// <overloads>
    /// <summary>
    /// Sets each vector component to its absolute value.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Sets each vector component to its absolute value.
    /// </summary>
    public void Absolute()
    {
      X = Math.Abs(X);
      Y = Math.Abs(Y);
      Z = Math.Abs(Z);
    }


    /// <overloads>
    /// <summary>
    /// Clamps the vector components to the range [min, max].
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Clamps the vector components to the range [min, max].
    /// </summary>
    /// <param name="min">The min limit.</param>
    /// <param name="max">The max limit.</param>
    /// <remarks>
    /// This operation is carried out per component. Component values less than 
    /// <paramref name="min"/> are set to <paramref name="min"/>. Component values greater than 
    /// <paramref name="max"/> are set to <paramref name="max"/>.
    /// </remarks>
    public void Clamp(float min, float max)
    {
      X = MathHelper.Clamp(X, min, max);
      Y = MathHelper.Clamp(Y, min, max);
      Z = MathHelper.Clamp(Z, min, max);
    }


    /// <overloads>
    /// <summary>
    /// Clamps near-zero vector components to zero.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Clamps near-zero vector components to zero.
    /// </summary>
    /// <remarks>
    /// Each vector component is compared to zero. If the component is in the interval 
    /// [-<see cref="Numeric.EpsilonF"/>, +<see cref="Numeric.EpsilonF"/>] it is set to zero, 
    /// otherwise it remains unchanged.
    /// </remarks>
    public void ClampToZero()
    {
      X = Numeric.ClampToZero(X);
      Y = Numeric.ClampToZero(Y);
      Z = Numeric.ClampToZero(Z);
    }


    /// <summary>
    /// Clamps near-zero vector components to zero.
    /// </summary>
    /// <param name="epsilon">The tolerance value.</param>
    /// <remarks>
    /// Each vector component is compared to zero. If the component is in the interval 
    /// [-<paramref name="epsilon"/>, +<paramref name="epsilon"/>] it is set to zero, otherwise it 
    /// remains unchanged.
    /// </remarks>
    public void ClampToZero(float epsilon)
    {
      X = Numeric.ClampToZero(X, epsilon);
      Y = Numeric.ClampToZero(Y, epsilon);
      Z = Numeric.ClampToZero(Z, epsilon);
    }


    /// <summary>
    /// Normalizes the vector.
    /// </summary>
    /// <remarks>
    /// A vectors is normalized by dividing its components by the length of the vector.
    /// </remarks>
    /// <exception cref="DivideByZeroException">
    /// The length of this vector is zero. The vector cannot be normalized.
    /// </exception>
    public void Normalize()
    {
      float length = Length;
      if (Numeric.IsZero(length))
        throw new DivideByZeroException("Cannot normalize a vector with length 0.");

      float scale = 1.0f / length;
      X *= scale;
      Y *= scale;
      Z *= scale;
    }


    /// <summary>
    /// Tries to normalize the vector.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the vector was normalized; otherwise, <see langword="false"/> if 
    /// the vector could not be normalized. (The length is numerically zero.)
    /// </returns>
    public bool TryNormalize()
    {
      float lengthSquared = LengthSquared;
      if (Numeric.IsZero(lengthSquared, Numeric.EpsilonFSquared))
        return false;

      float length = (float)Math.Sqrt(lengthSquared);

      float scale = 1.0f / length;
      X *= scale;
      Y *= scale;
      Z *= scale;

      return true;
    }


    /// <overloads>
    /// <summary>
    /// Projects a vector onto another vector.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Sets this vector to its projection onto the axis given by the target vector.
    /// </summary>
    /// <param name="target">The target vector.</param>
    public void ProjectTo(Vector3F target)
    {
      this = Dot(this, target) / target.LengthSquared * target;
    }


    /// <summary>
    /// Returns the cross product matrix (skew matrix) of this vector.
    /// </summary>
    /// <returns>The cross product matrix of this vector.</returns>
    /// <remarks>
    /// <c>Vector3F.Cross(v, w)</c> is the same as <c>v.ToCrossProductMatrix() * w</c>.
    /// </remarks>
    public Matrix33F ToCrossProductMatrix()
    {
      return new Matrix33F(0, -Z, Y,
                           Z, 0, -X,
                           -Y, X, 0);
    }
    #endregion


    //--------------------------------------------------------------
    #region Static Methods
    //--------------------------------------------------------------

    /// <summary>
    /// Returns a vector with the absolute values of the elements of the given vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>A vector with the absolute values of the elements of the given vector.</returns>
    public static Vector3F Absolute(Vector3F vector)
    {
      return new Vector3F(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
    }


    /// <overloads>
    /// <summary>
    /// Determines whether two vectors are equal (regarding a given tolerance).
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Determines whether two vectors are equal (regarding the tolerance 
    /// <see cref="Numeric.EpsilonF"/>).
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>
    /// <see langword="true"/> if the vectors are equal (within the tolerance 
    /// <see cref="Numeric.EpsilonF"/>); otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The two vectors are compared component-wise. If the differences of the components are less
    /// than <see cref="Numeric.EpsilonF"/> the vectors are considered as being equal.
    /// </remarks>
    public static bool AreNumericallyEqual(Vector3F vector1, Vector3F vector2)
    {
      return Numeric.AreEqual(vector1.X, vector2.X)
          && Numeric.AreEqual(vector1.Y, vector2.Y)
          && Numeric.AreEqual(vector1.Z, vector2.Z);
    }


    /// <summary>
    /// Determines whether two vectors are equal (regarding a specific tolerance).
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <param name="epsilon">The tolerance value.</param>
    /// <returns>
    /// <see langword="true"/> if the vectors are equal (within the tolerance 
    /// <paramref name="epsilon"/>); otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The two vectors are compared component-wise. If the differences of the components are less
    /// than <paramref name="epsilon"/> the vectors are considered as being equal.
    /// </remarks>
    public static bool AreNumericallyEqual(Vector3F vector1, Vector3F vector2, float epsilon)
    {
      return Numeric.AreEqual(vector1.X, vector2.X, epsilon)
          && Numeric.AreEqual(vector1.Y, vector2.Y, epsilon)
          && Numeric.AreEqual(vector1.Z, vector2.Z, epsilon);
    }


    /// <summary>
    /// Returns a vector with the vector components clamped to the range [min, max].
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="min">The min limit.</param>
    /// <param name="max">The max limit.</param>
    /// <returns>A vector with clamped components.</returns>
    /// <remarks>
    /// This operation is carried out per component. Component values less than 
    /// <paramref name="min"/> are set to <paramref name="min"/>. Component values greater than 
    /// <paramref name="max"/> are set to <paramref name="max"/>.
    /// </remarks>
    public static Vector3F Clamp(Vector3F vector, float min, float max)
    {
      return new Vector3F(MathHelper.Clamp(vector.X, min, max),
                          MathHelper.Clamp(vector.Y, min, max),
                          MathHelper.Clamp(vector.Z, min, max));
    }


    /// <summary>
    /// Returns a vector with near-zero vector components clamped to 0.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The vector with small components clamped to zero.</returns>
    /// <remarks>
    /// Each vector component (X, Y and Z) is compared to zero. If the component is in the interval 
    /// [-<see cref="Numeric.EpsilonF"/>, +<see cref="Numeric.EpsilonF"/>] it is set to zero, 
    /// otherwise it remains unchanged.
    /// </remarks>
    public static Vector3F ClampToZero(Vector3F vector)
    {
      vector.X = Numeric.ClampToZero(vector.X);
      vector.Y = Numeric.ClampToZero(vector.Y);
      vector.Z = Numeric.ClampToZero(vector.Z);
      return vector;
    }


    /// <summary>
    /// Returns a vector with near-zero vector components clamped to 0.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="epsilon">The tolerance value.</param>
    /// <returns>The vector with small components clamped to zero.</returns>
    /// <remarks>
    /// Each vector component (X, Y and Z) is compared to zero. If the component is in the interval 
    /// [-<paramref name="epsilon"/>, +<paramref name="epsilon"/>] it is set to zero, otherwise it 
    /// remains unchanged.
    /// </remarks>
    public static Vector3F ClampToZero(Vector3F vector, float epsilon)
    {
      vector.X = Numeric.ClampToZero(vector.X, epsilon);
      vector.Y = Numeric.ClampToZero(vector.Y, epsilon);
      vector.Z = Numeric.ClampToZero(vector.Z, epsilon);
      return vector;
    }


    /// <summary>
    /// Calculates the dot product of two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The dot product.</returns>
    /// <remarks>
    /// The method calculates the dot product (also known as scalar product or inner product).
    /// </remarks>
    public static float Dot(Vector3F vector1, Vector3F vector2)
    {
      return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
    }


    /// <summary>
    /// Calculates the cross product of two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The cross product.</returns>
    /// <remarks>
    /// The method calculates the cross product (also known as vector product or outer product).
    /// </remarks>
    public static Vector3F Cross(Vector3F vector1, Vector3F vector2)
    {
      Vector3F result;
      result.X = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
      result.Y = vector1.Z * vector2.X - vector1.X * vector2.Z;
      result.Z = vector1.X * vector2.Y - vector1.Y * vector2.X;
      return result;
    }


    /// <summary>
    /// Calculates the angle between two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The angle between the given vectors, such that 0 ≤ angle ≤ π.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="vector1"/> or <paramref name="vector2"/> has a length of 0.
    /// </exception>
    public static float GetAngle(Vector3F vector1, Vector3F vector2)
    {
      if (!vector1.TryNormalize() || !vector2.TryNormalize())
        throw new ArgumentException("vector1 and vector2 must not have 0 length.");

      float α = Dot(vector1, vector2);

      // Inaccuracy in the floating-point operations can cause
      // the result be outside of the valid range.
      // Ensure that the dot product α lies in the interval [-1, 1].
      // Math.Acos() returns Double.NaN if the argument lies outside
      // of this interval.
      α = MathHelper.Clamp(α, -1.0f, 1.0f);

      return (float) Math.Acos(α);
    }   


    /// <summary>
    /// Returns a vector that contains the lowest value from each matching pair of components.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector3F Min(Vector3F vector1, Vector3F vector2)
    {
      vector1.X = Math.Min(vector1.X, vector2.X);
      vector1.Y = Math.Min(vector1.Y, vector2.Y);
      vector1.Z = Math.Min(vector1.Z, vector2.Z);
      return vector1;
    }


    /// <summary>
    /// Returns a vector that contains the highest value from each matching pair of components.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The maximized vector.</returns>
    public static Vector3F Max(Vector3F vector1, Vector3F vector2)
    {
      vector1.X = Math.Max(vector1.X, vector2.X);
      vector1.Y = Math.Max(vector1.Y, vector2.Y);
      vector1.Z = Math.Max(vector1.Z, vector2.Z);
      return vector1;
    }


    /// <summary>
    /// Projects a vector onto an axis given by the target vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="target">The target vector.</param>
    /// <returns>
    /// The projection of <paramref name="vector"/> onto <paramref name="target"/>.
    /// </returns>
    public static Vector3F ProjectTo(Vector3F vector, Vector3F target)
    {
      return Dot(vector, target) / target.LengthSquared * target;
    }


    /// <overloads>
    /// <summary>
    /// Converts the string representation of a 3-dimensional vector to its <see cref="Vector3F"/>
    /// equivalent.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Converts the string representation of a 3-dimensional vector to its <see cref="Vector3F"/>
    /// equivalent.
    /// </summary>
    /// <param name="s">A string representation of a 3-dimensional vector.</param>
    /// <returns>
    /// A <see cref="Vector3F"/> that represents the vector specified by the <paramref name="s"/>
    /// parameter.
    /// </returns>
    /// <remarks>
    /// This version of <see cref="Parse(string)"/> uses the <see cref="CultureInfo"/> associated
    /// with the current thread.
    /// </remarks>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not a valid <see cref="Vector3F"/>.
    /// </exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public static Vector3F Parse(string s)
    {
      return Parse(s, CultureInfo.CurrentCulture);
    }


    /// <summary>
    /// Converts the string representation of a 3-dimensional vector in a specified culture-specific
    /// format to its <see cref="Vector3F"/> equivalent.
    /// </summary>
    /// <param name="s">A string representation of a 3-dimensional vector.</param>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information about
    /// <paramref name="s"/>. 
    /// </param>
    /// <returns>
    /// A <see cref="Vector3F"/> that represents the vector specified by the <paramref name="s"/>
    /// parameter.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not a valid <see cref="Vector3F"/>.
    /// </exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public static Vector3F Parse(string s, IFormatProvider provider)
    {
      Match m = Regex.Match(s, @"\((?<x>.*);(?<y>.*);(?<z>.*)\)", RegexOptions.None);
      if (m.Success)
      {
        return new Vector3F(float.Parse(m.Groups["x"].Value, provider),
          float.Parse(m.Groups["y"].Value, provider),
          float.Parse(m.Groups["z"].Value, provider));
      }

      throw new FormatException("String is not a valid Vector3F.");
    }
    #endregion
  }
}
