# -*- coding: utf-8 -*-
"""
Created on Wed Dec 20 09:56:52 2017

@author: 陈瑞达
"""
import xlrd
import numpy as np
import math
# =============================================================================
# 读取EXCEL数据
# =============================================================================
def readxls(file='data.xlsx'):
    xls = xlrd.open_workbook(file)
    table = xls.sheets()[0]
    ncols = table.ncols
    for i in range(ncols):
        data= table.col_values(i)
    return(data)
# =============================================================================
# 归一化
# =============================================================================
def scal(data):
    data_max=max(data)
    data_min=min(data)
    n=len(data)
    data_s=np.zeros(n)
    for i in range(n):
        data_s[i]=(data[i]-data_min)/(data_max-data_min)
    return data_s
# =============================================================================
#     灰色预测模型
# =============================================================================
def GM(data,alpha=0.5,pre=5):
    n=len(data)
    data_sum=np.zeros(n)
    #累加计算
    for i in range(n):
        data_sum[i]=sum(data[0:i+1]);
    B = np.zeros([n-1,2])
    Y = np.zeros([n-1,1])
    #系数矩阵计算
    for i in range(0,n-1):
       B[i][0]=-alpha*data_sum[i]+(alpha-1)*data_sum[i+1]
       B[i][1]=1
       Y[i][0]=data[i+1];
    u=np.zeros([2,1])
    u=np.linalg.inv(B.T.dot(B)).dot(B.T).dot(Y)
    a=u[0][0]
    b=u[1][0]
    x=np.zeros(n)
    x[0]=data[0]
    #计算验证值
    for i in range(1,n):
        x[i]=(data[0] - b/a)*(1-math.exp(a))*math.exp(-a*(i));
# =============================================================================
#     检验预测值
# =============================================================================
    e=np.zeros(n)#残差验证
    p=np.zeros(n)#级比偏差值检验
    lamda=np.zeros(n-1)#数列的级比 
    for i in range(0,n-1):
        lamda[i]=data[i]/data[i+1]
        if lamda[i]>math.exp(-2/(n+1)) or lamda[i]<math.exp(2/(n+1)):
            print('第'+str(i+1)+'个可以进行灰色预测')
            p[i]=1-(1-0.5*a)/(1+0.5*a)*lamda[i]
            if 0.1<abs(p[i])<0.2:
                print("第"+str(i)+'个达到级比偏差值检验的一般的要求')
            elif abs(p[i])<0.1:
                print("第"+str(i+1)+'个达到级比偏差值检验的较高的要求')
        else:
            print('第'+str(i+1)+'个需要进行平移变换')
    for i in range(0,n):
        e[i]= (data[i]-x[i])/data[i]
        if 0.1<abs(e[i])<0.2:
            print("第"+str(i+1)+'个达到残差验证的一般的要求')
        elif abs(e[i])<0.1:
            print("第"+str(i+1)+'个达到残差验证的较高的要求')
        else:
            print('不符合要求')
# =============================================================================
#      预测数据
# =============================================================================
    f = np.zeros(pre)
    for i in range(0,pre):
        f[i] = (data[0] - b/a)*(1-math.exp(a))*math.exp(-a*(i+n))
    return f