#include "invaders.h"
int main(){
const std::string applicationName = "Final Invaders";
const int DISPLAY_WIDTH = 640;
const int DISPLAY_HEIGHT = 480;
VGCVirtualGameConsole::initialize(applicationName, DISPLAY_WIDTH, DISPLAY_HEIGHT);
Invaders game;
game.run();
return 0;}
