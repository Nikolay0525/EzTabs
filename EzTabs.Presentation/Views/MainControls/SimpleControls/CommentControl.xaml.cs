using EzTabs.Presentation.Resources.Styles.CustomTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls
{
    public partial class CommentControl : UserControl
    {
        public CommentControl()
        {
            InitializeComponent();
        }

/*        public class LikeState
        {
            public Guid commentId;
            public bool isLiked;
        }
        
        public class Message
        {
            public Guid commentId;
            public string text;
        }*/

        public static readonly DependencyProperty AuthorIdProperty =
        DependencyProperty.Register("AuthorId", typeof(Guid), typeof(CommentControl), new PropertyMetadata(Guid.Empty));
        
        public static readonly DependencyProperty CommentIdProperty =
        DependencyProperty.Register("CommentId", typeof(Guid), typeof(CommentControl), new PropertyMetadata(Guid.Empty));
        
        public static readonly DependencyProperty ParentCommentIdProperty =
        DependencyProperty.Register("ParentCommentId", typeof(Guid), typeof(CommentControl), new PropertyMetadata(Guid.Empty));
        
        public static readonly DependencyProperty DateOfCreationProperty =
        DependencyProperty.Register("DateOfCreation", typeof(DateTime), typeof(CommentControl), new PropertyMetadata(DateTime.Now));

        public static readonly DependencyProperty UserNameProperty =
        DependencyProperty.Register("UserName", typeof(string), typeof(CommentControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CommentControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ReplyesListProperty =
        DependencyProperty.Register("ReplyesList", typeof(List<CommentControl>), typeof(CommentControl), new PropertyMetadata(new List<CommentControl>()));

        public static readonly DependencyProperty CanBeEditedProperty =
        DependencyProperty.Register("CanBeEdited", typeof(bool), typeof(CommentControl), new PropertyMetadata(false));

        public static readonly DependencyProperty LikedProperty =
        DependencyProperty.Register("Liked", typeof(bool), typeof(CommentControl), new PropertyMetadata(false));
        
        public static readonly DependencyProperty LikesProperty =
        DependencyProperty.Register("Likes", typeof(int), typeof(CommentControl), new PropertyMetadata(0));
        
        public static readonly DependencyProperty IsReplyesOpenedProperty =
        DependencyProperty.Register("IsReplyesOpened", typeof(bool), typeof(CommentControl), new PropertyMetadata(false));

        public Guid AuthorId
        {
            get { return (Guid)GetValue(AuthorIdProperty); }
            set { SetValue(AuthorIdProperty, value); }
        }
        
        public Guid CommentId
        {
            get { return (Guid)GetValue(CommentIdProperty); }
            set { SetValue(CommentIdProperty, value); }
        }
        
        public Guid ParentCommentId
        {
            get { return (Guid)GetValue(ParentCommentIdProperty); }
            set { SetValue(ParentCommentIdProperty, value); }
        }
        
        public DateTime DateOfCreation
        {
            get { return (DateTime)GetValue(DateOfCreationProperty); }
            set { SetValue(DateOfCreationProperty, value); }
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
      

        public List<CommentControl> ReplyesList
        {
            get { return (List<CommentControl>)GetValue(ReplyesListProperty); }
            set { SetValue(ReplyesListProperty, value); }
        }
        

        public bool CanBeEdited
        {
            get { return (bool)GetValue(CanBeEditedProperty); }
            set { SetValue(CanBeEditedProperty, value); }
        }
        
        public bool Liked
        {
            get { return (bool)GetValue(LikedProperty); }
            set { SetValue(LikedProperty, value); }
        }
        
        public int Likes
        {
            get { return (int)GetValue(LikesProperty); }
            set { SetValue(LikesProperty, value); }
        }
        
        public bool IsReplyesOpened
        {
            get { return (bool)GetValue(IsReplyesOpenedProperty); }
            set { SetValue(IsReplyesOpenedProperty, value); }
        }
    }
}
