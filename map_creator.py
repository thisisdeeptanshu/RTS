import pygame
import json

pygame.init()

win = pygame.display.set_mode((1280, 720))

temp = input("Type in a number for the speed (leave blank for default): ")
if temp == "":
    speed = 5
else:
    speed = int(temp)

pos = 0
size = 100
changeX = 0
changeY = 0

types = ["Grass", "Sand", "Water", "Tree", "Chest", "Player", "Delete"]
typeIndex = 0
currentType = types[typeIndex]

allBlocks = []
allObjects = []

def saveToFile(fileName):
    with open(fileName, "w") as f:
        obj = {
            "Squares": allBlocks,
            "Objects": allObjects
        }
        json.dump(obj, f)

running = True
while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
            break
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_s and pygame.key.get_mods() & pygame.KMOD_CTRL:
                    saveToFile("map_data.json")
                    print("map saved :)\nGo into the map_data.json file and replace '.0' with ''")
        elif event.type == pygame.MOUSEBUTTONDOWN:
            if event.button == 1: # Left
                pos = pygame.mouse.get_pos()
                pos = [pos[0], pos[1]]
                pos[0] -= changeX * speed
                pos[1] -= changeY * speed
                pos = [(i - i % size) / size for i in pos]
                print(f"Block type changed to {types[typeIndex]}")
            elif event.button == 3: # Right
                typeIndex = typeIndex + 1 if typeIndex < len(types) - 1 else 0
    win.fill((0, 0, 0))

    if pos != 0:
        if types[typeIndex] == "Delete":
            # Remove objects
            toRemove = None
            for i in range(len(allObjects)):
                if allObjects[i]["x"] == pos[0] and allObjects[i]["y"] == pos[1]:
                    toRemove = i
                    break
            if toRemove != None:
                allObjects.pop(toRemove)
                print(f"Removed block at {pos}.")
            else:
                # If object not found, remove block
                for i in range(len(allBlocks)):
                    if allBlocks[i]["x"] == pos[0] and allBlocks[i]["y"] == pos[1]:
                        toRemove = i
                        break
                if toRemove != None:
                    allBlocks.pop(toRemove)
                    print(f"Removed block at {pos}.")
        else:
            temp = types[typeIndex]
            if temp == "Grass" or temp == "Sand" or temp == "Water" or temp == "Player":
                # Remove any previous objects
                toRemove = None
                for i in range(len(allObjects)):
                    if allObjects[i]["x"] == pos[0] and allObjects[i]["y"] == pos[1]:
                        toRemove = i
                        break
                if toRemove != None:
                    allObjects.pop(toRemove)
                else:
                    # If object not found, remove block
                    for i in range(len(allBlocks)):
                        if allBlocks[i]["x"] == pos[0] and allBlocks[i]["y"] == pos[1]:
                            toRemove = i
                            break
                    if toRemove != None:
                        allBlocks.pop(toRemove)
            else:
                # Just remove the objects
                toRemove = None
                for i in range(len(allObjects)):
                    if allObjects[i]["x"] == pos[0] and allObjects[i]["y"] == pos[1]:
                        toRemove = i
                        break
                if toRemove != None:
                    allObjects.pop(toRemove)

            if temp == "Grass" or temp == "Sand" or temp == "Water" or temp == "Player":
                allObjects.append({"x": pos[0], "y": pos[1], "type": types[typeIndex]})
            else:
                allBlocks.append({"x": pos[0], "y": pos[1], "type": types[typeIndex]})
            print(f"Placed {types[typeIndex]} at {pos}")
        pos = 0
    
    keys=pygame.key.get_pressed()
    if keys[pygame.K_w] or keys[pygame.K_UP]: changeY += 1
    if keys[pygame.K_a] or keys[pygame.K_LEFT]: changeX += 1
    if keys[pygame.K_s] or keys[pygame.K_DOWN]: changeY -= 1
    if keys[pygame.K_d] or keys[pygame.K_RIGHT]: changeX -= 1
    
    for block in allBlocks + allObjects:
        colour = (255, 255, 255)
        if block["type"] == "Grass": colour = (0, 255, 0)
        if block["type"] == "Sand": colour = (255, 100, 0)
        if block["type"] == "Water": colour = (0, 0, 255)
        if block["type"] == "Tree": colour = (150, 75, 0)
        if block["type"] == "Chest": colour = (25, 25, 25)
        if block["type"] == "Player": colour = (255, 0, 0)
        pygame.draw.rect(win, colour, pygame.Rect(block["x"] * size + changeX * speed, block["y"] * size + changeY * speed, size, size))
    pygame.display.update()
pygame.display.quit()
saveToFile("yousavedright_questionmark_.json")
