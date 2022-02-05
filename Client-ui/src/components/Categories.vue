<template>
  <div class="container">
    <h1>Categories</h1>
    <Tree :nodes="categoryTree" />
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import Tree from 'vuejs-tree'
//https://openbase.com/js/vuejs-tree/documentation

export default {
  name: 'Categories',
  data() {
    return {
      treeDisplayData: [
        {
          text: 'Root 1',
          state: { checked: false, selected: false, expanded: false },
          nodes: [
            {
              text: 'Child 1',
              state: { checked: false, selected: false, expanded: false }
            }
          ]
        },
        {
          text: 'Root 2',
          state: { checked: false, selected: false, expanded: false }
        }
      ]
    }
  },
  components: {
    'Tree': Tree
  },
  computed: {
    ...mapState(['categories']),
    topLevelCategories() {
      return this.categories.filter(category => 
        category.parentCategoryId == 0
      );
    },
    categoryTree() {
      return this.topLevelCategories.map(category => {
        category.nodes = this.BuildTree(category)
        category = this.convertCategoryView(category)
        return category
      });
    },
  },
  methods: {
    ...mapActions(['addCategory']),
    BuildTree(parentNode) {
      var children = this.categories.filter(c => c.parentCategoryId == parentNode.id);
      children.forEach(childNode => {
        childNode.nodes = this.BuildTree(childNode);
      });
      return children.map(child => {
        return this.convertCategoryView(child)
      });
    },
    convertCategoryView(category) {
      category.state = {
        checked: false,
        expanded: false,
        selected: false,
      }
      category.text = category.name
      return category
    },
  }
}
</script>