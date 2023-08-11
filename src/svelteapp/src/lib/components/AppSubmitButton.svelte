<script lang="ts">
  export let className = "";
</script>

<button class="btn variant-ghost-surface {className}" type="submit">
  <slot />
</button>
