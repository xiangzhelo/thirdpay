using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// ����״̬
    /// �Ƿ���1ȫ���ر� 2ȫ������4  ������ Ĭ�Ϲرգ�����������̻������õ�״̬ ���δ���ã� 8 ������ Ĭ�Ϲر�
    /// </summary>
    [Serializable]
    public enum OpenEnum
    {
        None = 0,
        AllClose = 1,
        AllOpen  = 2,
        Close    = 4,
        Open     = 8
    }
}
