# -*- coding: utf-8 -*-
"""
Created on Sun Dec 17 21:48:40 2017

@author: 陈瑞达
"""
import numpy as np
from svmutil import *
import matplotlib.pyplot as plt
def bestsvc(train_label,train_vector,cmin=-8,cmax=8,gmin=-8,gmax=8,pmin=-8,pmax=8,v=5,cstep=0.8,gstep=0.8,pstep=0.8,t=2,msestep=0.06):
    bestc=0
    bestg=0
    bestp=0
    mse=float("inf")
    basenum=2
    eps =pow(10,-4)
    x = np.linspace(cmin, cstep, cmax)
    y = np.linspace(gmin, gstep, gmax)
    z = np.linspace(pmin, pstep, pmax)
    [X,Y,Z]= np.meshgrid(x,y,z)
    [x_n,y_n,z_n]=(X.shape)  
    cg = X
    for i in range(0,x_n):
        for j in range(0,y_n):
            for r in range(0,z_n):
                cmd = ['-v '+str(v)+' -c '+str( pow(basenum,X[i,j,r]))+' -g '+str( pow(basenum,Y[i,j,r]) )+' -p '+str(pow(basenum,Z[i,j,r]) )+' -t 2 -s 3']
                cmd=''.join(cmd)
                cg[i,j,r]=svm_train(train_label, train_vector, cmd)
                if cg[i,j,r]<mse:
                    mse = cg[i,j,r]
                    bestc=pow(basenum,X[i,j,r])
                    bestg=pow(basenum,Y[i,j,r])
                    bestp=pow(basenum,Z[i,j,r])
                if abs(cg[i,j,r]-mse)<=eps and bestc>pow(basenum,X[i,j,r]):
                    mse=cg[i,j,r]
                    bestc=pow(basenum,X[i,j,r])
                    bestg=pow(basenum,Y[i,j,r])
                    bestp=pow(basenum,Z[i,j,r])
    return bestc,bestg,bestp

                    