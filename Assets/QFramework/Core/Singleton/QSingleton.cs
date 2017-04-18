﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 1.泛型
/// 2.反射
/// 3.抽象类
/// 4.命名空间
/// </summary>
namespace QFramework {
	public abstract class QSingleton<T> :ISingleton where T : QSingleton<T>
	{
		protected static T mInstance = null;

		protected QSingleton()
		{
		}

		public static T Instance
		{

			get 
			{
				if (mInstance == null) 
				{
					// 先获取所有非public的构造方法
					ConstructorInfo[] ctors = typeof(T).GetConstructors (BindingFlags.Instance | BindingFlags.NonPublic);
					// 从ctors中获取无参的构造方法
					ConstructorInfo ctor = Array.Find (ctors, c => c.GetParameters ().Length == 0);
					if (ctor == null) {
						Debug.LogWarning ("Non-public ctor() not found!");
						ctors = typeof(T).GetConstructors (BindingFlags.Instance | BindingFlags.Public);
						ctor = Array.Find (ctors, c => c.GetParameters ().Length == 0);
						mInstance = ctor.Invoke(null) as T;
					} else {
						// 调用构造方法
						mInstance = ctor.Invoke (null) as T;
					}
				}

				return mInstance;
			}
		}

		public static T ResetInstance()
		{
			mInstance = null;
			mInstance = Instance;
			mInstance.OnSingletonInit();
			return mInstance;
		}


		public void Dispose()
		{
			mInstance = null;
		}

		public virtual void OnSingletonInit()
		{
		}
	}
}