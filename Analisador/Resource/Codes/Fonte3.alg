action
goto -desvios


r11 - TIPO  --> literal
 r8 -    L  --> id
 r6 -    D  --> TIPO L pt_v
 r9 - TIPO  --> inteiro
 r8 -    L  --> id
 r7 -    L  --> id vir L
 r7 -    L  --> id vir L
 r6 -    D  --> TIPO L pt_v
r10 - TIPO  --> real
 r8 -    L  --> id
 r6 -    D  --> TIPO L pt_v
 r5 -   LV  --> varfim pt_v
 r4 -   LV  --> D LV
 r4 -   LV  --> D LV
 r4 -   LV  --> D LV
 r3 -    V  --> varinicio LV
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r13 -   ES  --> leia id pt_v
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r13 -   ES  --> leia id pt_v
r22 - OPRD  --> id
r23 - OPRD  --> num
r27 - EXP_R  --> OPRD opr OPRD
r26 -  CAB  --> se ab_p EXP_R fc_p então
r22 - OPRD  --> id
r23 - OPRD  --> num
r27 - EXP_R  --> OPRD opr OPRD
r26 -  CAB  --> se ab_p EXP_R fc_p então
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r31 -   CP  --> fimse
r28 -   CP  --> ES CP
r25 - COND  --> CAB CP
r31 -   CP  --> fimse
r30 -   CP  --> COND CP
r25 - COND  --> CAB CP
r22 - OPRD  --> id
r22 - OPRD  --> id
r20 -   LD  --> OPRD opr OPRD
r19 -  CMD  --> id rcb LD pt_v
r22 - OPRD  --> id
r22 - OPRD  --> id
r20 -   LD  --> OPRD opr OPRD
r19 -  CMD  --> id rcb LD pt_v
r22 - OPRD  --> id
r22 - OPRD  --> id
r20 -   LD  --> OPRD opr OPRD
r19 -  CMD  --> id rcb LD pt_v
r22 - OPRD  --> id
r21 -   LD  --> OPRD
r19 -  CMD  --> id rcb LD pt_v
r23 - OPRD  --> num
r21 -   LD  --> OPRD
r19 -  CMD  --> id rcb LD pt_v
r22 - OPRD  --> id
r22 - OPRD  --> id
r20 -   LD  --> OPRD opr OPRD
r19 -  CMD  --> id rcb LD pt_v
r17 -  ARG  --> id
r14 -   ES  --> escreva ARG pt_v
r22 - OPRD  --> id
r22 - OPRD  --> id
r20 -   LD  --> OPRD opr OPRD
r19 -  CMD  --> id rcb LD pt_v
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r17 -  ARG  --> id
r14 -   ES  --> escreva ARG pt_v
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r17 -  ARG  --> id
r14 -   ES  --> escreva ARG pt_v
r15 -  ARG  --> lit
r14 -   ES  --> escreva ARG pt_v
r17 -  ARG  --> id
r14 -   ES  --> escreva ARG pt_v
r32 -    A  --> fim
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
r18 -    A  --> CMD A
r12 -    A  --> ES A
r18 -    A  --> CMD A
r18 -    A  --> CMD A
r18 -    A  --> CMD A
r18 -    A  --> CMD A
r18 -    A  --> CMD A
r18 -    A  --> CMD A
r24 -    A  --> COND A
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
r12 -    A  --> ES A
 r2 -    P  --> inicio V A