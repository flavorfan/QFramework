/****************************************************************************
 * Copyright (c) 2017 xiaojun
 * Copyright (c) 2017 liangxie
 * 
 * http://liangxiegame.com
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 ****************************************************************************/

namespace QFramework
{
	using System;

	/// <summary>
	/// msgbody
	/// </summary>
	public class QMsg 
	{
		// 表示 65535个消息 占两个字节
		// for proto buf;
		public virtual int msgId { get; set; }

		public int GetMgrID()
		{
			int tmpId = msgId / QMsgSpan.Count;

			return tmpId * QMsgSpan.Count;
		}

		public QMsg() {}

		public QMsg(int msgId)
		{
			this.msgId = msgId;
		}
	}

	public class QEventMsg<T> :QMsg where T : IConvertible 
	{
		public QEventMsg(T msgId) : base(msgId.ToUInt16(null)) {}
	}

	[Obsolete("请使用 QMsgWithValue<T> ")]
	public class QMsgWithUIPageData: QMsg 
	{
		public UIPageData Data;

		public QMsgWithUIPageData(ushort msgId,UIPageData data) :base(msgId) 
		{
			this.Data = data;
		}
	}

	[Obsolete("请使用 QMsgWithValue<T> ")]
	public class QMsgWithStr  :QMsg 
	{
		public string strMsg;

		public QMsgWithStr(ushort msgId,string strMsg) :base(msgId) 
		{
			this.strMsg = strMsg;
		}
	}

	public class QMsgWithValue<T> : QMsg
	{
		public T value;

		public QMsgWithValue(ushort msgId, T value) : base(msgId)
		{
			this.value = value;
		}
	}
}