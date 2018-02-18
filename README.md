# ListFrames2

**Description**

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

On iOS the Slots are not rendered (or invisible?). Displays a blank open space where the Slots should have been rendered.

Not using groups 

**Code @**
https://github.com/gregmercer/ListFrames2

**Stack Trace**

na

**Steps to Reproduce**
- When App opens the FramesPage gets OnAppearing. Shows page from heirarchy show above.
- Tab to second page - OtherPage.
- Tab back to Frames page.

**Actual Behavior**
- Scroll. Notice Slots are not being rendered (or invisible?).

**Expected Behavior**
- The Slots should be displaying correctly for each ListView row.

**Some findings:**

Running with local build of Xamarin Forms found that if I changed the RendererPool.cs UpdateNewElement method to have the 
the following new code it seemed to cause the Slots to render correctly:

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

<tested code change>

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

</tested code change>
```

**Basic Information**

Version with issue: - Include="Xamarin.Forms" Version="2.5.0" a356efc
Last known good version: - New issue

IDE: - Visual Studio

Platform Target Frameworks:
iOS: 
Android: Works fine

Nuget Packages:
Xamarin Forms

Affected Devices:
IOS emulator and iPhone 6 (real device)

Screenshots
image
