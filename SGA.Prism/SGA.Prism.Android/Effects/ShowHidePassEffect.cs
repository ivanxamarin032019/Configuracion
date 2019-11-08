using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(SGA.Prism.Droid.Effects.ShowHidePassEffect), "ShowHidePassEffect")]

namespace SGA.Prism.Droid.Effects
{
    public class ShowHidePassEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
        }

        private void ConfigureControl()
        {
            //EditText editText = ((EditText)Control);
            //editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);
            //editText.SetOnTouchListener(new OnDrawableTouchListener());

            EditText editText = ((EditText)Control);
            //editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.ic_person_white, 0, Resource.Drawable.ShowPass, 0);
            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);

            editText.SetOnTouchListener(new OnDrawableTouchListener());

        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;
                if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        //editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.ic_person_white, 0, Resource.Drawable.ShowPass, 0);
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);

                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        //editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.ic_person_white, 0, Resource.Drawable.HidePass, 0);
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.HidePass, 0);

                    }
                    return true;
                }
            }
            return false;
        }
    }

}