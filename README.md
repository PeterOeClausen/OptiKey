# Experimental version of OptiKey for conducting gaze typing experiments.

The 'Experimental version of OptiKey' project (this project) is a modified version of [**OptiKey**](https://github.com/OptiKey/OptiKey/wiki) specifically developed for performing gaze typing experiments with OptiKey.

With the experimental version of OptiKey, you are able to specify the parameters of your gaze typing experiments, perform the experiments with OptiKey, and have the program log important data such as which buttons is being looked at with milliseconds precision, or eye tracking data such as pupil diameter while typing. This information is logged into .CSV format files, which can easily be analyzed after conducting experiments.

In the image below, you can see the parameters you are able to specify:
![Image of the ExperimentalMenu could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/Manual/ImagesForManual/ExperimentMenu.png "ExperimentalMenu")

The settings you specify in the ExperimentMenu are saved automatically, so that you can perform multiple experiments with the same saved settings.

Clicking "START EXPERIMENT", in the bottom of the window will start the experiment:
![Image of the gaze typing keyboard could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/manual/ImagesForManual/TheKeyboard.png "The gaze typing keyboard")

During the experiment a user types in phrases shown cyan, e.g. "The quick brown fox jumps over the lazy dog" as shown in the image above. 

The program logs which phrases are shown for the user together with a time stamp:
![Image of PhraseLog.csv could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/manual/ImagesForManual/PhraseLog.PNG "PhraseLog.csv")

The program also logs which keys the user activates:
![Image of KeySelectionLog.csv could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/manual/ImagesForManual/KeySelectionLog.PNG "KeySelectionLog.csv")

What is written into the scratchpad in blue, e.g. "The quick brown fox jum":
![Image of ScratchPadLog.csv could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/manual/ImagesForManual/ScratchPadLog.PNG "ScratchPadLog.csv")

And also which screen elements the user looks at:
![Image of UserLooksAtKeyLog.csv could not be shown](https://github.com/PeterOeClausen/OptiKey/blob/master/manual/ImagesForManual/UserLooksAtKeyLog.PNG "UserLooksAtKeyLog.csv")

Right now the experimental version of OptiKey works with a mouse, and the same eye trackers that works with OptiKey. Gaze tracking data can however right now only be logged when using the EyeTribe Development Kit. However we are working on supporting other trackers as well.

# Getting Started

You can get a manual for the experimental OptiKey project here: [**Manual on the experimental version of OptiKey**](https://github.com/PeterOeClausen/OptiKey/raw/master/Manual/Manual%20on%20the%20experimental%20version%20of%20OptiKey.pdf).

# Supported Platforms

OptiKey targets the .Net 4.6 Framework, which is available for  Windows Vista SP2 onwards. It was designed to run on Windows 8/ 8.1/ 10.

# License

Licensed under the GNU GENERAL PUBLIC LICENSE (Version 3, 29th June 2007)

# Contact

If you have any questions about the project, if you want to get in touch, you can write an email to me: <PeterOeClausen@gmail.com>.

If you want to contact the creator of OptiKey, see the [**OptiKey.org**](https://github.com/OptiKey/OptiKey/wiki).

Best,
Peter Ø. Clausen - Research assistent and student developer at research project [**GazeIT**](http://www.cachet.dk/research/research-projects/gaze-it).
