#include "Eigen" 
#include <iostream> 
#include <math.h> 
#include <SFML\Graphics.hpp> //width 0.2, height 0.1 
//center of gravity(centerposition) is 0.04 from the lower left corner in the x axis and O.02 in the y axis 
//corners are named from upper left to bottom rigtht 							   a b  
//c is at position 0.0 but the relavant number is the relative position to the centerposition              c d 
//centerposition is at position  x = 0.04 y = 0.02 
//width 0.2, height 0.1 
// aposition = 0,0.1 - centerposition = -0.04,0.08   
//bposition = 0.2,0.1 - 0.04,0.02 = 0.16,0.08 
//cposition = 0,0 - 0.04,0.02 = -0.04, -0.02 
//dposition = 0.2,0 - 0.04,0.02 = 0.16,-0.02 
static float windowWidth = 800; 
static float windowHeight = 600; //values for the window in sfml
//moves values from an eigen double vectors to sfml float vector 

sf::Vector2f eigenToSFML(const Eigen::Vector2d &inputVector) 
{   //the values are changed in order to better visualize them, the base values are too small and some of them are below zero
float x = inputVector(0);  
float y = inputVector(1);//implicit conversion    x += 2;  y += 2;
 x /= 4;  y /= 4;  x *= windowWidth;  y *= windowHeight;  return sf::Vector2f(x,y ); } 
int main() {   
 
 
Eigen::Vector2d relativeAposition(-0.04, 0.08);  
Eigen::Vector2d relativeBposition(0.16,  0.08);  
Eigen::Vector2d relativeCposition(-0.04,-0.02);  
Eigen::Vector2d relativeDposition(0.16, -0.02);  
double startingAngle = 40 * (M_PI / 180);///degrees is 40, then its changed to radians, 
used to launch object not used to rotate corners  
double angleSpeed = 200 * (M_PI / 180);//same type of operation as above (200 degrees)
//the starting speed is 2
double Vy0 = 2 * sin(startingAngle);  
double Vx0 = 2 * cos(startingAngle);  
double g = 9.82;  
double frameLength = 0.01;  
double Tmax = Vy0 / g * (1 + std::sqrt(1 + 2 * (1.2*g) / (Vy0*Vy0)));  //the tmax was to be determined as a result of the 
int pointNumber = Tmax / frameLength + 1;  
sf::VertexArray centralLine(sf::LinesStrip,pointNumber);  
sf::VertexArray Aline(sf::Points, pointNumber);  
sf::VertexArray Bline(sf::Points, pointNumber);  
sf::VertexArray Cline(sf::Points, pointNumber);  
sf::VertexArray Dline(sf::Points, pointNumber); 
 
 
 double t = 0;//creating the variables before the loop, the loop will set the values differently each time  
double currentRotation = 0;//these are the variables that will change  
Eigen::Vector2d centralPosition(0,0);  
Eigen::Vector2d currentAposition(0,0);  
Eigen::Vector2d currentBposition(0,0);  
Eigen::Vector2d currentCposition(0,0);  
Eigen::Vector2d currentDposition(0,0);  
Eigen::Matrix2d rotationMatrix;  rotationMatrix << 0, 0, 0, 0;  
int i = 0;//for adding values to the sfml vector  
for (double t = 0; t < Tmax;t+=frameLength)  
{   centralPosition(0,0) = Vx0 * t;//xvalue   
centralPosition(1, 0) = 1.2 + Vy0*t - g*t*t / 2;//yvalue  
 std::cout  << centralPosition << std::endl << "t = " << t << std::endl;   
currentRotation = t*angleSpeed ;   
rotationMatrix(0, 0) = cos(currentRotation);   
rotationMatrix(1, 0) = -sin(currentRotation);   
rotationMatrix(0, 1) = sin(currentRotation);  
rotationMatrix(1, 1) = cos(currentRotation);   
currentAposition = rotationMatrix*relativeAposition+centralPosition;   
currentBposition = rotationMatrix*relativeBposition + centralPosition;   
currentCposition = rotationMatrix*relativeCposition + centralPosition; 
currentDposition = rotationMatrix*relativeDposition + centralPosition;   
//now for adding the points to the drawing vector   
centralLine[i].position = eigenToSFML(centralPosition);   
centralLine[i].color = sf::Color::White;   
Aline[i].position = eigenToSFML(currentAposition);   
Aline[i].color = sf::Color::Blue;   
Bline[i].position = eigenToSFML(currentBposition);   
Bline[i].color = sf::Color::Green;   
Cline[i].position = eigenToSFML(currentCposition);   
Cline[i].color = sf::Color::Magenta;   
Dline[i].position = eigenToSFML(currentDposition);   
Dline[i].color = sf::Color::Red;   i++;  }  
//cornerposition = central position + relativeposition * rotation matrix   is the fundamental equation for determinating the position of the corners
//visualization of the data gathered  
sf::VideoMode Video1(windowWidth, windowHeight);  
sf::RenderWindow window(Video1, "My window");  
sf::View view(sf::FloatRect(0, windowHeight, windowWidth,windowHeight));//sfml uses a different y display so we have to flip it  
window.setView(view);  
while (window.isOpen())  {   
 sf::Event event1;   
while (window.pollEvent(event1))   {       
if (event1.type == sf::Event::Closed)     
{
 window.close();    
}   
}      
window.clear();   
window.draw(Aline);   
window.draw(Bline);   
window.draw(Cline);   
window.draw(Dline);   
window.draw(centralLine); 
 
  window.display();  } 
 
 std::cin.get();  return 0; } 
 
 
 
 
