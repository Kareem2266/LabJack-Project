import u3
from tkinter import *
from tkinter import ttk


# the constructor for the u3 class will try to automatically open the first found u3 LabJack
LabJack = u3.U3()

# the calibration data will be used by functions that convert binary data to voltage
LabJack.getCalibrationData()

# read AIN0 input
voltage1 = LabJack.getAIN(1)

# read AIN1 input
voltage2 = LabJack.getAIN(0)

voltage_resistance = voltage1 - voltage2

current = (voltage2 / 100) * 1000  # v2 *10 but we want to be fancy

resistor = voltage_resistance / current

# read AIN1 input
# voltage2 = LabJack.getFeedback(u3.BitStateRead(1))

# print(f'voltage 1 is = {voltage1:.2f}')
# print(f'voltage 2 is = {voltage2:.2f}')

# make the gui
window = Tk()
window.configure(background="black")
# to rename the title of the window
window.title("GUI LabJack")

# photo
photo1 = PhotoImage(file="D:\\Kareem.png")
Label(window, image=photo1, bg="black").grid(row=0, column=2, sticky=N)


# key down function
def click():
    # Label(window, text=f'The first voltage is = {voltage1:.2f}', bg="black", fg="white",
    # font="none 10 bold").grid(row=40, column=1, sticky=W)

    # Label(window, text=f'The second voltage is = {voltage2:.2f}', bg="black", fg="white",
    # font="none 10 bold").grid(row=80, column=1, sticky=W)

    Label(window, text=f'Source Voltage = {voltage_resistance:.2f} volt', bg="black", fg="white",
          font="none 10 bold").grid(row=40, column=1, sticky=W)

    Label(window, text=f'Current = {current:.2f} mA', bg="black", fg="white",
          font="none 10 bold").grid(row=80, column=1, sticky=W)

    Label(window, text=f'Resistor Value = {resistor:.2f} kΩ', bg="black", fg="white",
          font="none 10 bold").grid(row=120, column=1, sticky=W)


# key down function
def click2():
    exit()


# Photo
Label(window, bg="black")

# label
Label(window, text="Welcome to my DAQ U3-HV Program:", bg="black", fg="white", font="none 25 bold").grid(row=0,
                                                                                                         column=0,
                                                                                                         sticky=N)

Label(window, text="Press start to get the voltages and press stop to stop the program", bg="black", fg="white",
      font="none 10 bold").grid(row=1, column=0, sticky=N)

# add a button
Button(window, text="RUN", width=6, command=click).grid(row=30, column=0, sticky=W)

Button(window, text="STOP", width=6, command=click2).grid(row=140, column=0, sticky=W)

# add the label for the click

window.mainloop()
