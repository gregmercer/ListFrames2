# ListFrames2

**Description**

On iOS the Slots are not rendered (or invisible?). Displays a blank open space where the Slots should have been rendered.

**UI Structure**

```
Xamarin Forms 
  Content Page
    StackLayout
      ListView
        ItemTemplate 
          DataTemplate
            StackLayout
              Label
              Label
              TimeSlotsWrapLayout (Layout<View>)
                BindingProperty (TimeSlotsSourceProperty TimeSlots)
                  Frame (Random number of Frames - Slots)
                    Label 
```                  

Not using groups 

**Code @**
https://github.com/gregmercer/ListFrames2

**Stack Trace**

na

**Steps to Reproduce**
- When App opens the FramesPage gets OnAppearing. Shows page with UI structure heirarchy shown above.
- Tab to second page - OtherPage.
- Tab back to Frames page.

**Actual Behavior**
- Scroll. Notice Slots are not being rendered (or invisible?).

**Expected Behavior**
- The Slots should be displaying correctly for each ListView row.

**Some findings:**

> Running with local build of Xamarin Forms I've found that if I changed the 

> RendererPool.cs UpdateNewElement method to have the following new testing code it 

> seemed to cause the Slots to render correctly:

```
public void UpdateNewElement(VisualElement newElement)

    ...

      if (oldChildren.Count == newChildren.Count)
      {
        for (var i = 0; i < oldChildren.Count; i++)
        {
          if (oldChildren[i].GetType() != newChildren[i].GetType())
          {
            sameChildrenTypes = false;
            break;
          }
        }
      }
      else
        sameChildrenTypes = false;

<testing code added>

      if (sameChildrenTypes) 
      {
          if (newElement is StackLayout)
          {
              //sameChildrenTypes = false;
              ClearRenderers(_parent);
              FillChildrenWithRenderers(newElement);
              return;
          }
      }

</testing code added>
```

**Basic Information**

> Version with issue: - Include="Xamarin.Forms" Version="2.5.0" a356efc
Last known good version: - New issue

> IDE: - Visual Studio

> Platform Target Frameworks:
iOS: Renders incorectly, or invisible.
Android: Works fine

**Nuget Packages:**
Xamarin Forms

**Affected Devices:**
IOS emulator and iPhone 6 (real device)

Screenshots
image
